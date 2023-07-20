using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Interfaces;
using Windows.Storage.Pickers;

namespace ViewModel
{
    public sealed class MainViewModel : ViewModelBase
    {
        private ICommand selectFilePathCommand;

        private IFilePathSelector pathSelector;

        public ObservableCollection<FileViewModel> Files { get; } = new ObservableCollection<FileViewModel>();

        public ICommand SelectFilePathCommand => this.selectFilePathCommand ?? (this.selectFilePathCommand = new RelayCommand(this.SelectFilePath));

        public MainViewModel(IFilePathSelector pathSelector)
        {
            this.pathSelector = pathSelector;
        }

        private void SelectFilePath()
        {
            string filePath = this.pathSelector.GetFilePath();
            if (!string.IsNullOrWhiteSpace(filePath))
                this.Files.Add(new FileViewModel(new FileModel(filePath) { Status = FileStatus.Selected }));
        }
    }
}