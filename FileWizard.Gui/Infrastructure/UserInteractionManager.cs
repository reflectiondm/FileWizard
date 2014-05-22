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
        private Window _parentWindow;
        public UserInteractionManager(Window parentWindow)
        {
            _parentWindow = parentWindow;
        }

        public bool AskUserConfirmation(string confirmationMessage)
        {
            var result = MessageBox.Show(confirmationMessage, "Please confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            return result == MessageBoxResult.Yes;
        }


        public void ShowModalDialog(IViewModel dialogViewModel, string title)
        {
            //Probably in future IDialogViewModel interface should be introduced,
            //with dialog-related properties defined such as title, icon, etc.
            //for now that is enough
            var window = new Window();
            window.Title = title;
            window.Content = dialogViewModel;
            window.ShowInTaskbar = false;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.ResizeMode = ResizeMode.NoResize;
            window.Owner = _parentWindow;
            window.Icon = _parentWindow.Icon;
            window.ShowDialog();
        }
    }
}
