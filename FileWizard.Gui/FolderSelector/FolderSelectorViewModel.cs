using FileWizard.Gui.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileWizard.Gui.FolderSelector
{
    public class FolderSelectorViewModel : ViewModelBase
    {
        public FolderSelectorViewModel()
        {
            _goToNextStepCommand = new DelegateCommand(d => GoToNextStep(), d => CanGoToNextStep());
            _cancelCommand = new DelegateCommand(d => {});
        }

        private string _folderPath;
        private DelegateCommand _goToNextStepCommand;
        private DelegateCommand _cancelCommand;
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
        { }

        private bool CanGoToNextStep()
        {
            var result = string.IsNullOrEmpty(FolderPath) ?
                false :
                true;
            return result;
        }
    }
}
