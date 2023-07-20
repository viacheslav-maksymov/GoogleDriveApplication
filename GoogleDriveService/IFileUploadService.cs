using Google.Apis.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IFileUploadService
    {
        Task<bool> UploadFileToGoogleDriveAsync(string filePath, string fileName, Action<IUploadProgress> progressChanged);
    }
}
