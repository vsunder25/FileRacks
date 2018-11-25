using System;
using System.Windows.Input;

namespace FileRacks.UI.ViewModel
{
    public class RelayCommand : ICommand
    {
        #region Object Management        
        public RelayCommand(Action<object> executeMethod) : this(executeMethod, null)
        {

        }

        public RelayCommand(Action<object> executeMethod, Predicate<object> canExecuteMethod)
        {
            if (executeMethod != null)
            {
                _executeMethod = executeMethod;
                _canExecuteMethod = canExecuteMethod;
            }
        }
        #endregion

        #region Properties
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Methods        
        public bool CanExecute(object parameter)
        {
            return (null == _canExecuteMethod) || _canExecuteMethod(parameter);
        }

        public void Execute(object parameter)
        {
            _executeMethod(parameter);
        }
        #endregion

        #region Private
        private Predicate<object> _canExecuteMethod;
        private Action<object> _executeMethod;
        #endregion
    }
}