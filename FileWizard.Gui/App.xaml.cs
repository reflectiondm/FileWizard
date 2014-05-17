using FileWizard.Gui.FolderSelector;
using FileWizard.Gui.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FileWizard.Gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// All the bootstrapping will be handled here.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            INavigationManager navigationManager = null;
            var folderSelectorViewModel = new FolderSelectorViewModel(navigationManager);
            var mainWindowViewModel = new MainWindowViewModel(folderSelectorViewModel, navigationManager);

            mainWindow.DataContext = mainWindowViewModel;

            mainWindow.Show();
        }
    }
}
