namespace ContactsClient.Commands
{
    using ContactsClient.ViewModels;

    /// <summary>
    /// Command for deleting contact.
    /// </summary>
    public class DeleteContactCommand : BaseTypedCommand<MainWindowVM>
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

            mainWindowVM.ContactService.DeleteContact(contactVM);
            mainWindowVM.ContactVMs.Remove(contactVM);
            mainWindowVM.Update();
        }
    }
}