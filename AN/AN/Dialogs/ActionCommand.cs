using System;
using System.Windows.Input;
namespace AN.Dialogs
{
    public class ActionCommand : ICommand
    {
        private readonly Action<object> action;
        private readonly Predicate<Object> predicate;

        public ActionCommand(Action<Object> action) : this(action, null)
        {
        }

        public ActionCommand(Action<Object> action, Predicate<Object> predicate)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), @"You must specify an Action<T>.");
            }

            this.action = action;
            this.predicate = predicate;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public bool CanExecute(object parameter)
        {
            if (this.predicate == null)
            {
                return true;
            }
            return this.predicate(parameter);
        }


        public void Execute()
        {
            Execute(null);
        }

        public void Execute(object parameter)
        {
            this.action(parameter);
        }
    }
}