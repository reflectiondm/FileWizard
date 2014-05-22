using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Infrastructure
{
    public interface IUserInteractionManager
    {
        bool AskUserConfirmation(string confirmationMessage);

        void ShowModalDialog(IViewModel dialogViewModel, string title);
    }
}
