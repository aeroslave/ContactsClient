namespace ContactsClient.Commands
{
    using ContactsClient.Models;
    using ContactsClient.ViewModels;
    using ContactsClient.Windows;

    /// <summary>
    /// Command for adding contact.
    /// </summary>
    public class AddContactCommand : BaseTypedCommand<MainWindowVM>
    {
        /// <summary>
        /// Executing command.
        /// </summary>
        /// <param name="mainWindowVM">View-model of main window.</param>
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            if (mainWindowVM.SelectedContactVM == null)
                return;

            var contactVM = new ContactVM();
            var editContactWindow = new EditContactWindow { DataContext = contactVM };
            if (editContactWindow.ShowDialog() != true)
                return;

            mainWindowVM.ContactService.CreatePerson(contactVM);
            mainWindowVM.ContactVMs.Add(contactVM);
            mainWindowVM.Update();
        }
    }
}