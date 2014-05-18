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
        private INavigationManager _navigationManager;

        public FolderSelectorViewModel(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
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
            _navigationManager.GoToNextView();
        }

        private bool CanGoToNextStep()
        {
            var result = string.IsNullOrEmpty(FolderPath) ?
                false :
                true;
            return result;
        }
    }
}
