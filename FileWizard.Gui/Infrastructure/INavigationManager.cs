using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Infrastructure
{
    public interface INavigationManager
    {
        void GoToNextView();

        void GoToPreviousView();

        void CloseWindow();

        event EventHandler OnToNextStep;

        event EventHandler OnToPreviousStep;
    }
}
