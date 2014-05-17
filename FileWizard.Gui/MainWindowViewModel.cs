using FileWizard.Gui.FolderSelector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IViewModel folderSelectorVM)
        {
            ActiveViewModel = folderSelectorVM;
        }

        public MainWindowViewModel()
            : this(new FolderSelectorViewModel(null))
        {

        }

        private IViewModel _activeViewModel;
        public IViewModel ActiveViewModel
        {
            get
            {
                return _activeViewModel;
            }
            set
            {
                if (_activeViewModel == value)
                    return;

                _activeViewModel = value;
                RaiseOnPropertyChanged("ActiveViewModel");
            }
        }
    }
}
