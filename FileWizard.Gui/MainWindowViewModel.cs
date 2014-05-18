using FileWizard.Gui.WizardSteps;
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
        private IViewModel _activeViewModel;
        private INavigationManager _navigationManager;
        private IViewModel[] _steps;
        private int _currentIndex;

        public MainWindowViewModel(IEnumerable<IViewModel> viewModels, INavigationManager navigationManager)
        {
            if (viewModels == null)
                throw new ArgumentNullException("viewModels");
            _steps = viewModels.ToArray();
            if (_steps.Length == 0)
                throw new ArgumentException("viewModels must have at least one element");
            ActiveViewModel = _steps[0];
            _currentIndex = 0;
            _navigationManager = navigationManager;
            _navigationManager.OnToNextStep += _navigationManager_OnToNextStep;
            _navigationManager.OnToPreviousStep += _navigationManager_OnToPreviousStep;
            
        }

        void _navigationManager_OnToPreviousStep(object sender, EventArgs e)
        {
            var newIndex = _currentIndex - 1;
            GoToStepAndUpdateIndex(newIndex);
        }

        void _navigationManager_OnToNextStep(object sender, EventArgs e)
        {
            var newIndex = _currentIndex + 1;
            GoToStepAndUpdateIndex(newIndex);
        }

        private void GoToStepAndUpdateIndex(int newIndex)
        {
            if (newIndex < _steps.Length && newIndex >= 0)
            {
                _currentIndex = newIndex;
                ActiveViewModel = _steps[_currentIndex];
            }
        }

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
