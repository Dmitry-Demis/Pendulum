using Rope.Interfaces;
using Rope.Model;
using Rope.Services;
using Rope.View;
using Rope.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Rope
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IDialogService dialogService = new DialogService(MainWindow);
            IFileService fileService = new JsonFileService();
            dialogService.Register<LW1_PendulumViewModel, LW1_PendulumWindow>();
            dialogService.Register<LW2_PendulumViewModel, LW2_PendulumWindow>();
            var viewModel = new MainWindowViewModel(dialogService, fileService);
            var view = new MainWindow { DataContext = viewModel };
            view.ShowDialog();
        }
    }
}
