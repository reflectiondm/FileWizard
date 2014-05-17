using FileWizard.Gui.FolderSelector;
using FileWizard.Gui.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IViewModel viewModel, INavigationManager navigationManager)
        {
            ActiveViewModel = viewModel;
            _navigationManager = navigationManager;
        }

        private IViewModel _activeViewModel;
        private INavigationManager _navigationManager;
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
