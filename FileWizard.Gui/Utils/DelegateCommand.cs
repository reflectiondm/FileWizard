using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileWizard.Gui.Utils
{
    /// <summary>
    /// Delegate-based ICommand implementation, mimic the one introduced in Prism library.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private Action<object> _executeAction;
        private Func<object, bool> _canExecuteAction;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteAction = null)
        {
            if (executeAction == null)
                throw new ArgumentNullException("executeAction");
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteAction == null)
                return true;

            return _canExecuteAction(parameter);
        }

        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }
    }
}
