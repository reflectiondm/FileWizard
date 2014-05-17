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
            GoToNextStepCommand = new DelegateCommand(d => GoToNextStep(), d => CanGoToNextStep());
            CancelCommand = new DelegateCommand(d => {});
        }

        private string _folderPath;
        public string FolderPath
        {
            get
            {
                return _folderPath;
            }
            set
            {
                _folderPath = value;
                RaiseOnPropertyChanged("FolderPath");
            }
        }

        public ICommand GoToNextStepCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

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
