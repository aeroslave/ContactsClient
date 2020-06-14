namespace ContactsClient.Commands
{
    using ContactsClient.ViewModels;
    using ContactsClient.Windows;

    public class AddGroupCommand : BaseTypedCommand<MainWindowVM>
    {
        public override void Execute(MainWindowVM mainWindowVM)
        {
            var createGroupWindowVM = new CreateGroupWindowVM();
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