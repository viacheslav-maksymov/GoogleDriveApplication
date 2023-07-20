using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;

namespace Service
{
    public sealed class GoogleDriveService : IFileUploadService, IDisposable
    {
        private const string ApplicationName = "GoogleDriveApplication";

        private readonly UserCredential credential;

        private readonly DriveService driveService;

        public void Dispose()
        {
            this.driveService?.Dispose();
        }

        public GoogleDriveService()
        {
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                this.credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { DriveService.Scope.Drive },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            this.driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = GoogleDriveService.ApplicationName,
            });
        }

        public async Task<bool> UploadFileToGoogleDriveAsync(string filePath, string fileName, Action<IUploadProgress> progressChanged)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Open))
            {
                request = this.driveService.Files.Create(fileMetadata, stream, "application/octet-stream");
                request.Fields = "id";
                request.ProgressChanged += progressChanged;
                await request.UploadAsync();
            }

            return true;
        }
    }
}