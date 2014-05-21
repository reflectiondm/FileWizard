using FileWizard.Gui.Infrastructure;
using FileWizard.Gui.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileWizard.Gui.WizardSteps
{
    public class FolderSelectorViewModel : ViewModelBase
    {
        private string _folderPath;
        private DelegateCommand _goToNextStepCommand;
        private DelegateCommand _cancelCommand;
        private readonly INavigationManager _navigationManager;
        private readonly IFileRepository _fileRepository;

        public FolderSelectorViewModel(INavigationManager navigationManager, IFileRepository fileRepository)
        {
            _navigationManager = navigationManager;
            _fileRepository = fileRepository;
            _goToNextStepCommand = new DelegateCommand(d => GoToNextStep(), d => CanGoToNextStep());
            _cancelCommand = new DelegateCommand(d => Cancel());
        }

        private void Cancel()
        {
            _navigationManager.CloseWindow();
        }

       
        public string FolderPath
        {
            get
            {
                return _folderPath;
            }
            set
            {
                _folderPath = value;
                _goToNextStepCommand.RaiseCanExecuteChanged();
                RaiseOnPropertyChanged("FolderPath");
            }
        }

        public ICommand GoToNextStepCommand { get { return _goToNextStepCommand; } }
        public ICommand CancelCommand { get { return _cancelCommand; } }

        private void GoToNextStep()
        {
            _navigationManager.ChooseFolder(FolderPath);
            _navigationManager.GoToNextView();
        }

        private bool CanGoToNextStep()
        {
            if (string.IsNullOrEmpty(FolderPath) ||
                !_fileRepository.DoesFolderExist(FolderPath))
                return false;

            return true;
        }
    }
}
