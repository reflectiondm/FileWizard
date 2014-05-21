using FileWizard.Gui.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWizard.Gui.Tests.Fakes
{
    public class FakeUserInteractionManager : IUserInteractionManager
    {
        public bool UserConfirmationAsked { get; set; }

        public bool AskUserConfirmation(string confirmationMessage)
        {
            UserConfirmationAsked = true;

            return false;
        }
    }
}
