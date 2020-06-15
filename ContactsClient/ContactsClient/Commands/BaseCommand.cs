namespace ContactsClient.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Base class for command.
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        /// <summary>
        /// Check if can executing command.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>Always true/</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Executing command.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public abstract void Execute(object parameter);
    }
}