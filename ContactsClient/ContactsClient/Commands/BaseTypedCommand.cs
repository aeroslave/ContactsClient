namespace ContactsClient.Commands
{
    /// <summary>
    /// Base class for typed command.
    /// </summary>
    /// <typeparam name="T">Type of parameter.</typeparam>
    public abstract class BaseTypedCommand<T> : BaseCommand
    {
        /// <summary>
        /// Executing command.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public override void Execute(object parameter)
        {
            if (parameter is T typeParameter)
                Execute(typeParameter);
        }

        /// <summary>
        /// Executing command.
        /// </summary>
        /// <param name="parameter">Typed command parameter.</param>
        protected abstract void Execute(T parameter);
    }
}