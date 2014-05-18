using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Infrastructure
{
    public class NavigationManager : INavigationManager
    {
        public NavigationManager(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void GoToNextView()
        {
            OnToNextStep(this, EventArgs.Empty);
        }

        public void GoToPreviousView()
        {
            OnToPreviousStep(this, EventArgs.Empty);
        }

        public void CloseWindow()
        {
            _mainWindow.Close();
        }

        public event EventHandler OnToNextStep = delegate { };

        public event EventHandler OnToPreviousStep = delegate { };
        private MainWindow _mainWindow;
    }
}
