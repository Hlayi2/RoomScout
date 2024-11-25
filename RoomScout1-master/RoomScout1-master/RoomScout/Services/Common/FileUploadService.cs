using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomScout.Services.Common
{
    public class FileUploadService : IFileUploadService
    {
        private readonly string[] allowedFileTypes = { ".pdf", ".jpg", ".jpeg", ".png" };
        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB

        public async Task<Models.AdminSide.UploadedFile> UploadFileAsync(FileResult file, string purpose)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (!await ValidateFileAsync(file))
                throw new InvalidOperationException("Invalid file type or size");

            string localFilePath = Path.Combine(FileSystem.AppDataDirectory,
                                              $"{purpose}_{DateTime.Now.Ticks}_{file.FileName}");

            using (var stream = await file.OpenReadAsync())
            using (var fileStream = File.OpenWrite(localFilePath))
            {
                await stream.CopyToAsync(fileStream);
            }

            return new Models.AdminSide.UploadedFile
            {
                FileName = file.FileName,
                FilePath = localFilePath,
                FileType = Path.GetExtension(file.FileName),
                FileSize = new FileInfo(localFilePath).Length,
                UploadDate = DateTime.Now,
                IsCertified = false
            };
        }

        public async Task<bool> ValidateFileAsync(FileResult file)
        {
            if (file == null) return false;

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedFileTypes.Contains(extension))
                return false;

            using (var stream = await file.OpenReadAsync())
            {
                if (stream.Length > MaxFileSize)
                    return false;
            }


            return true;

        }
    }
}
