using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomScout.Services.Common
{
    public interface IFileUploadService
    {
        Task<Models.AdminSide.UploadedFile> UploadFileAsync(FileResult file, string purpose);
        Task<bool> ValidateFileAsync(FileResult file);
    }
}
