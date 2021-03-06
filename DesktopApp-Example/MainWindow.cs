﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesktopApp_Example.DTO;
using DesktopApp_Example.Helpers;
using DesktopApp_Example.Services;
using Inzynierka_Core.Model;

namespace DesktopApp_Example
{
    public partial class MainWindow : Form
    {
        private readonly AuthData _authData;
        private readonly IFileService _fileService;
        private ViewFile _selectedFile;
        private readonly Timer _tokenTimer = new Timer();

        public MainWindow()
        {
            InitializeComponent();

            var serverLogin = new ServerLogin();
            serverLogin.ShowDialog();
            _authData = serverLogin.AuthData;
            if (_authData == null)
                Environment.Exit(-1);

            var fileServerSelect = new FileServiceSelect();
            fileServerSelect.ShowDialog();
            var fileService = fileServerSelect.ServiceName;

            if (fileService == null)
                Environment.Exit(-1);

            try
            {
                switch (fileService)
                {
                    case "GoogleDrive":
                        _fileService = new GoogleDriveFileService();
                        break;
                    
                    case "OneDrive":
                        _fileService = new OneDriveFileService();
                        break;

                    default:
                        _fileService = new OwnServerFileService(_authData);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Aby korzystać z aplikacji należy sie zalogować i zatwierdzić dostęp aplikacji do danych",
                    "Brak autoryzacji", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Environment.Exit(-1);
            }

            _tokenTimer.Interval = 5000;
            _tokenTimer.Tick += TokenTimerOnTick;
            _tokenTimer.Start();
        }

        private void TokenTimerOnTick(object sender, EventArgs e)
        {
            var validDate = (new DateTime(1970, 1, 1)).AddSeconds(_authData.TokenExpirationTime);
            if (validDate < DateTime.Now)
            {
                _tokenTimer.Stop();
                MessageBox.Show("Ważność twojego tokena skonczyła się, zaloguj się ponownie!", "Token nie ważny",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Environment.Exit(-1);
            }
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            var loader = new Loader("Pobieranie listy plików","Trwa pobieranie listy plików, proszę czekać!");
            loader.ControlBox = false;
            loader.Show();
            loader.Owner = this;
            SwitchFormEnabled(false);
            try
            {
                await RefreshFileList();
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd podczas pobierania listy plików z serwera. Sprobój ponownie pózniej!",
                    "Błąd pobierania listy plików", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Environment.Exit(-1);
            }
            Invoke(new Action(loader.Close));
            Invoke(new Action<bool>(SwitchFormEnabled),true);
        }

        private async void buttonUpload_Click(object sender, EventArgs e)
        {
            var shareFile = new ShareFile(_authData.Token,_authData.Email);
            var shareFileResult = shareFile.ShowDialog();

            if (shareFileResult == DialogResult.OK)
            {
                var receiverList = new List<Receiver>
                {
                    new Receiver(_authData.Email, _authData.RsaKeys.MapToRsaParameters())
                };

                foreach (var shareFileSelectedUser in shareFile.SelectedUsers)
                {
                    receiverList.Add(new Receiver(shareFileSelectedUser.Email, new RSAParameters
                    {
                        Modulus = shareFileSelectedUser.Modulus,
                        Exponent = shareFileSelectedUser.Exponent
                    }));
                }

                var isShared = shareFile.SelectedUsers.Count > 0;
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 0;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var fileName = openFileDialog.SafeFileName?.Split('.').First();
                        var fileExtension = Path.GetExtension(openFileDialog.FileName);
                        using (var fileStream = openFileDialog.OpenFile() as FileStream)
                        {
                            var loader = new Loader("Uploadowanie w toku", "Trwa uploadowanie pliku, proszę czekać!");
                            loader.ControlBox = false;
                            loader.Owner = this;
                            loader.Show();
                            SwitchFormEnabled(false);
                            ShareLinksDto shareLinks = null;
                            try
                            {
                                shareLinks = await _fileService.UploadFile(fileName, fileExtension, fileStream, receiverList, _authData.RsaKeys.MapToRsaParameters(), isShared);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Błąd podczas dodawania pliku na serwer. Sprobój ponownie pózniej!",
                                    "Błąd dodawania pliku", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                Invoke(new Action(loader.Close));
                                Invoke(new Action<bool>(SwitchFormEnabled), true);
                                return;
                            }
                            await RefreshFileList();
                            if (isShared)
                            {
                                var linksToShareWindow = new LinksToShare(shareLinks.JsonFileLink, shareLinks.EncryptedFileLink);
                                linksToShareWindow.ControlBox = false;
                                linksToShareWindow.Owner = this;
                                Invoke(new Action(linksToShareWindow.Show));
                            }
                            Invoke(new Action(loader.Close));
                            Invoke(new Action<bool>(SwitchFormEnabled), true);
                            MessageBox.Show("Plik został poprawnie zapisany na dysku w chmurze", "Plik zapisany",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void listBoxFiles_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var index = listBoxFiles.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                listBoxFiles.SelectedIndex = index;
                _selectedFile = listBoxFiles.Items[index] as ViewFile;
                fileContextMenuStrip.Show(Cursor.Position);
                fileContextMenuStrip.Visible = true;
            }
            else
            {
                fileContextMenuStrip.Visible = false;
            }
        }

        private async void pobierzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter ="All files (*.*)|";
                saveFileDialog.FileName = _selectedFile.Name;
                var splitedName = _selectedFile.Name.Split('.');
                saveFileDialog.DefaultExt = splitedName.Length > 1 ? splitedName[1] : null;
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var loader = new Loader("Pobieranie w toku","Trwa pobieranie pliku, proszę czekać!");
                    loader.ControlBox = false;
                    loader.Owner = this;
                    loader.Show();
                    SwitchFormEnabled(false);
                    using (var fileStream = saveFileDialog.OpenFile())
                    {
                        Stream stream = null;
                        try
                        {
                            stream = await _fileService.DownloadFile(_selectedFile, _authData.Email, _authData.RsaKeys.MapToRsaParameters());
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Błąd podczas pobierania pliku z serwera. Sprobój ponownie pózniej!",
                                "Błąd pobierania pliku", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            Invoke(new Action(loader.Close));
                            Invoke(new Action<bool>(SwitchFormEnabled),true);
                            return;
                        }

                        stream.Position = 0;
                        await stream.CopyToAsync(fileStream);
                    }
                    Invoke(new Action(loader.Close));
                    Invoke(new Action<bool>(SwitchFormEnabled),true);
                    MessageBox.Show("Plik został pobrany pomyślnie do katalogu " + saveFileDialog.FileName, "Plik pobrany",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private async void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var loader = new Loader("Usuwanie w toku","Trwa usuwanie pliku, proszę czekać!");
            loader.ControlBox = false;
            loader.Owner = this;
            loader.Show();
            SwitchFormEnabled(false);
            try
            {
                await _fileService.DeleteFile(_selectedFile);
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd podczas usuwania pliku z serwera. Sprobój ponownie pózniej!",
                    "Błąd usuwania pliku", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Invoke(new Action(loader.Close));
                Invoke(new Action<bool>(SwitchFormEnabled),true);
                return;
            }
            try
            {
                await RefreshFileList();
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd podczas pobierania listy plików z serwera. Sprobój ponownie pózniej!",
                    "Błąd pobieranie listy plików", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Environment.Exit(-1);
            }
            Invoke(new Action(loader.Close));
            Invoke(new Action<bool>(SwitchFormEnabled),true);
            MessageBox.Show("Plik został usunięty", "Plik usunięty",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task RefreshFileList()
        {
            Invoke(new Action(listBoxFiles.Items.Clear));
            var files = await _fileService.GetAllFiles();
            foreach (var viewFile in files)
            {
                Invoke(new Func<object,int>(listBoxFiles.Items.Add), viewFile);
            }
        }

        private void SwitchFormEnabled(bool isActive)
        {
            listBoxFiles.Enabled = isActive;
            buttonUpload.Enabled = isActive;
            fileContextMenuStrip.Enabled = isActive;
            buttonDownloadShared.Enabled = isActive;
        }

        private void buttonDownloadShared_Click(object sender, EventArgs e)
        {
            var downloadShared = new DownloadShared(_authData,_fileService);
            downloadShared.Owner = this;
            downloadShared.Show();
        }
    }
}
