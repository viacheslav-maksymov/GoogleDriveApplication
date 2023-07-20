using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Interfaces;

namespace Views
{
    public class FilePathSelector : IFilePathSelector
    {
        public string GetFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл";

            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileName;

            return string.Empty;
        }
    }
}
