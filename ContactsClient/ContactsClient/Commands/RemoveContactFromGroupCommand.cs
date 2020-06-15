namespace ContactsClient.Commands
{
    using ContactsClient.ViewModels;

    /// <summary>
    /// Command for removing contact from group.
    /// </summary>
    public class RemoveContactFromGroupCommand : BaseTypedCommand<MainWindowVM>
    {
        /// <summary>
        /// Executing command.
        /// </summary>
        /// <param name="mainWindowVM">View-model of main window.</param>
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            var groupName = mainWindowVM.SelectedGroup;

            if (string.IsNullOrWhiteSpace(groupName))
                return;

            mainWindowVM.ContactService.ModifyContactGroup(mainWindowVM.SelectedContactVM, groupName, true);
            mainWindowVM.Update();
        }
    }
}