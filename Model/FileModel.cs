namespace Model
{
    public sealed class FileModel
    {
        public string FileName => Path.GetFileName(this.FullPath);

        public string FilePath => Path.GetDirectoryName(this.FullPath);

        public string FullPath { get; set; }

        public FileStatus Status { get; set; }

        public string StatusString => this.Status.ToString();

        public FileModel(string path)
        {
            this.FullPath = path;
        }

    }
}