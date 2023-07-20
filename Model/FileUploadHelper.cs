using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public sealed class FileUploadHelper
    {
        private static IFileUploadService fileUploadService;

        public static IFileUploadService FileUploadService => FileUploadHelper.fileUploadService ?? (FileUploadHelper.fileUploadService = new GoogleDriveService());
    }
}
