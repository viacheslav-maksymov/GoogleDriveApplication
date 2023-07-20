using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Google.Apis.Upload;
using Model;
using System.Windows.Input;


namespace ViewModel
{
    public sealed class FileViewModel : ViewModelBase
    {
        private FileModel file;
        private ICommand uploadFileCommand;
        private long fileSize;
        private long bytesSent;

        public long FileSize 
        {
            get => this.fileSize;
            set
            {
                this.fileSize = value;
                this.RaisePropertyChanged();
            }
        }

        public long BytesSent
        {
            get => this.bytesSent;
            set
            {
                this.bytesSent = value;
                this.RaisePropertyChanged();
            }
        }

        public FileViewModel(FileModel file)
        {
            this.file = file;
            this.FileSize = new FileInfo(this.file.FullPath).Length;
        }

        public string FilePath
        {
            get => this.file.FullPath;
            set
            {
                this.file.FullPath = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.FileSize));
            }
        }

        public string StatusString => this.file.StatusString;

        public bool IsNotUploaded => this.file.Status != FileStatus.Uploaded;

        private void SetStatus(FileStatus status)
        {
            this.file.Status = status;
            this.RaisePropertyChanged(nameof(this.StatusString));
            this.RaisePropertyChanged(nameof(this.IsNotUploaded));
        }

        public ICommand UpploadFileCommand => this.uploadFileCommand ?? (this.uploadFileCommand = new RelayCommand(this.UploadFile));

        private async void UploadFile()
        {
            this.SetStatus(FileStatus.Uploading);
            bool isSuccesfull = await FileUploadHelper.FileUploadService.UploadFileToGoogleDriveAsync(this.file.FilePath, this.file.FileName, this.ProgressChanged);

            FileStatus status = isSuccesfull ? FileStatus.Uploaded : FileStatus.Failed;
            this.SetStatus(status);
        }

        private void ProgressChanged(IUploadProgress progress)
        {
            this.BytesSent = progress.BytesSent;
        }
    }
}
