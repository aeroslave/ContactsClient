namespace ContactsClient.Commands
{
    public abstract class BaseTypedCommand<T>: BaseCommand
    {
        public override void Execute(object parameter)
        {
            if (parameter is T typeParameter)
                Execute(typeParameter);
        }

        public abstract void Execute(T parameter);
    }
}