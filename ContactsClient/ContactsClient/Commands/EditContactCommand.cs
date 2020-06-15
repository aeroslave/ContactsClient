namespace ContactsClient.Commands
{
    using ContactsClient.ViewModels;
    using ContactsClient.Windows;

    /// <summary>
    /// Command for editing contact.
    /// </summary>
    public class EditContactCommand: BaseTypedCommand<MainWindowVM>
    {
        /// <summary>
        /// Executing command.
        /// </summary>
        /// <param name="mainWindowVM">View-model of main window.</param>
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            var contactVM = mainWindowVM.SelectedContactVM;
            if (contactVM == null)
                return;

            var editContactWindow = new EditContactWindow { DataContext = contactVM };
            if (editContactWindow.ShowDialog() != true)
                return;
            
            mainWindowVM.ContactService.UpdateContact(contactVM);
            mainWindowVM.Update();
        }
    }
}