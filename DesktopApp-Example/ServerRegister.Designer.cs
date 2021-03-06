﻿namespace DesktopApp_Example
{
    partial class ServerRegister
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelRegisterErrors = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(270, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Email";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(270, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Hasło";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonRegister
            // 
            this.buttonRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRegister.Enabled = false;
            this.buttonRegister.Location = new System.Drawing.Point(3, 417);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(270, 29);
            this.buttonRegister.TabIndex = 3;
            this.buttonRegister.Text = "Zarejestruj się";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxEmail.Location = new System.Drawing.Point(3, 169);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(270, 20);
            this.textBoxEmail.TabIndex = 5;
            this.textBoxEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxEmail.TextChanged += new System.EventHandler(this.textBox_Changed);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPassword.Location = new System.Drawing.Point(3, 241);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(270, 20);
            this.textBoxPassword.TabIndex = 6;
            this.textBoxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBox_Changed);
            // 
            // labelRegisterErrors
            // 
            this.labelRegisterErrors.AutoSize = true;
            this.labelRegisterErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRegisterErrors.ForeColor = System.Drawing.Color.Red;
            this.labelRegisterErrors.Location = new System.Drawing.Point(3, 0);
            this.labelRegisterErrors.Name = "labelRegisterErrors";
            this.labelRegisterErrors.Size = new System.Drawing.Size(270, 137);
            this.labelRegisterErrors.TabIndex = 7;
            this.labelRegisterErrors.Text = "label4";
            this.labelRegisterErrors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxPassword, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBoxEmail, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonRegister, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelRegisterErrors, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.65661F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.511137F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.495408F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.782435F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.906154F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.60868F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.039571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(276, 449);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLoading.Enabled = false;
            this.pictureBoxLoading.ImageLocation = "Assets/loadingCircle.gif";
            this.pictureBoxLoading.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(276, 449);
            this.pictureBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxLoading.TabIndex = 11;
            this.pictureBoxLoading.TabStop = false;
            this.pictureBoxLoading.Visible = false;
            // 
            // ServerRegister
            // 
            this.AcceptButton = this.buttonRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 449);
            this.Controls.Add(this.pictureBoxLoading);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ServerRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rejestracja";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelRegisterErrors;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
    }
}