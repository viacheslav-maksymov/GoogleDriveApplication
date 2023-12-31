﻿using System.Windows;
using ViewModel;
using Views;

namespace GoogleDriveApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = new MainViewModel(new FilePathSelector());
        }
    }
}
