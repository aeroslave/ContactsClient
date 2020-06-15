namespace ContactsClient.Commands
{
    using System.Collections.Generic;

    using ContactsClient.ViewModels;
    using ContactsClient.Windows;

    /// <summary>
    /// Command for adding contact to group.
    /// </summary>
    public class AddContactToGroupCommand : BaseTypedCommand<MainWindowVM>
    {
        /// <summary>
        /// Executing command.
        /// </summary>
        /// <param name="mainWindowVM">View-model of main window.</param>
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            var selectedContact = mainWindowVM.SelectedContactVM;
            var selectGroupWindowVM = new SelectGroupWindowVM
            {
                GroupNames = new List<string>(mainWindowVM.GroupNames)
            };

            var selectGroupWindow = new SelectGroupWindow { DataContext = selectGroupWindowVM };

            if (selectGroupWindow.ShowDialog() != true)
                return;

            mainWindowVM.ContactService.ModifyContactGroup(selectedContact, selectGroupWindowVM.GroupName, false);
            selectedContact.GroupList.Add(selectGroupWindowVM.GroupName);
        }
    }
}