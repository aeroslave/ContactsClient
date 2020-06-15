namespace ContactsClient.Commands
{
    using System.Windows;

    using ContactsClient.Constants;
    using ContactsClient.ViewModels;

    /// <summary>
    /// Command for deletinig contact group.
    /// </summary>
    public class DeleteGroupCommand : BaseTypedCommand<MainWindowVM>
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

            if (groupName == SystemGroupNames.ALL_CONTACTS)
            {
                MessageBox.Show("You can't delete this group");
                return;
            }

            mainWindowVM.ContactService.DeleteGroup(groupName);
            mainWindowVM.GroupNames.Remove(groupName);

            mainWindowVM.Update();
        }
    }
}