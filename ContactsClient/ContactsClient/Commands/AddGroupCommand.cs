namespace ContactsClient.Commands
{
    using ContactsClient.ViewModels;
    using ContactsClient.Windows;

    /// <summary>
    /// Command for adding contact group.
    /// </summary>
    public class AddGroupCommand : BaseTypedCommand<MainWindowVM>
    {
        /// <summary>
        /// Executing command.
        /// </summary>
        /// <param name="mainWindowVM">View-model of main window.</param>
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            var createGroupWindowVM = new GroupWindowVM();
            var createGroupWindow = new CreateGroupWindow
            {
                DataContext = createGroupWindowVM
            };

            if (createGroupWindow.ShowDialog() != true)
                return;

            var groupName = createGroupWindowVM.GroupName;
            mainWindowVM.GroupNames.Add(groupName);
            mainWindowVM.ContactService.CreateGroup(groupName);
        }
    }
}