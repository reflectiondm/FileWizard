using FileWizard.Gui.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Tests.Fakes
{
    public class NavigationManagerMock : INavigationManager
    {
        public void GoToNextView()
        {
            GoNextWasCalled = true;
        }

        public void GoToPreviousView()
        {
            throw new NotImplementedException();
        }

        public void CloseWindow()
        {
            CloseWindowWasCalled = true;
        }

        public event EventHandler OnToNextStep;

        public event EventHandler OnToPreviousStep;

        public bool GoNextWasCalled { get; set; }

        public bool CloseWindowWasCalled { get; set; }
    }
}
