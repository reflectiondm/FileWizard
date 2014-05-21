using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileWizard.Gui.Infrastructure
{
    public class UserInteractionManager : IUserInteractionManager
    {
        public bool AskUserConfirmation(string confirmationMessage)
        {
            var result = MessageBox.Show(confirmationMessage, "Please confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            return result == MessageBoxResult.Yes;
        }
    }
}
