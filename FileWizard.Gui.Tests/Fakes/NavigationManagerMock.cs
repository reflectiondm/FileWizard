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
            OnToNextStep(this, EventArgs.Empty);
        }

        public void GoToPreviousView()
        {
            OnToPreviousStep(this, EventArgs.Empty);
        }

        public void CloseWindow()
        {
            CloseWindowWasCalled = true;
        }

        public event EventHandler OnToNextStep = delegate { };

        public event EventHandler OnToPreviousStep = delegate { };

        public bool GoNextWasCalled { get; set; }

        public bool CloseWindowWasCalled { get; set; }



        public void ChooseFolder(string path)
        {
            ChosenFolder = path;
            OnFolderChosen(this, new FolderChosenEventArgs(path));
        }

        public event EventHandler<FolderChosenEventArgs> OnFolderChosen = delegate { };

        public string ChosenFolder { get; set; }
    }
}
