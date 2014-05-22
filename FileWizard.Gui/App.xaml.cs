using FileWizard.Gui.WizardSteps;
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
            INavigationManager navigationManager = new NavigationManager(mainWindow);
            IFileRepository fileRepository = new FileRepository();
            IUserInteractionManager userInterationManager = new UserInteractionManager(mainWindow);
            var folderSelectorViewModel = new FolderSelectorViewModel(navigationManager, fileRepository);
            var fileListViewModel = new FileListViewModel(navigationManager, fileRepository, userInterationManager);
            var viewModels = new IViewModel[] 
            { 
                folderSelectorViewModel, 
                fileListViewModel 
            };

            var mainWindowViewModel = new MainWindowViewModel(viewModels, navigationManager);

            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();
        }
    }
}
