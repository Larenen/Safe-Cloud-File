﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServer_Example.Data;
using ApiServer_Example.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServer_Example.Services
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _context;

        public FileRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<FileModel> CreateFileAsync(FileModel fileModel)
        {
            if (fileModel == null) throw new ArgumentNullException(nameof(fileModel));

            var addedFile = _context.Files.Add(fileModel);
            if(await _context.SaveChangesAsync() <= 0) throw new Exception("Error while adding new fileModel in DB");

            return addedFile.Entity;
        }

        public async Task<bool> DeleteFileAsync(Guid id)
        {
            if(id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(id));
            var file = _context.Files.SingleOrDefault(f => f.Id == id);
            if(file == null) throw new ArgumentException("No FileModel with such ID found!");

            _context.Files.Remove(file);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<FileModel> UpdateFile(FileModel fileModel)
        {
            if (fileModel == null) throw new ArgumentNullException(nameof(fileModel));
            var result = _context.Files.Update(fileModel);
                
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<IEnumerable<FileModel>> GetAllUserFiles(Guid userId)
        {
            if(userId.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(userId));

            return await _context.Files.AsNoTracking().Where(f => f.UserId == userId).ToListAsync();
        }

        public async Task<FileModel> GetFileByIdAsync(Guid id)
        {
            if(id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(id));
            return await _context.Files.AsNoTracking().SingleOrDefaultAsync(f => f.Id == id);
        }

        public async Task<FileModel> GetFileByNameAsync(string fileName, string fileType)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));

            return await _context.Files.AsNoTracking().SingleOrDefaultAsync(f => f.FileName == fileName && f.FileType == fileType);
        }

        public async Task<bool> CheckIfFileExist(string fileName, string fileType)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));

            return await _context.Files.AsNoTracking().AnyAsync(f => f.FileName == fileName && f.FileType == fileType);
        }
    }
}
