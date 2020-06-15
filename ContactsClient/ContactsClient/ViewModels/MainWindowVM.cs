namespace ContactsClient.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using ContactsClient.Commands;
    using ContactsClient.Constants;
    using ContactsClient.ContactService;

    /// <summary>
    /// ViewModel for main window.
    /// </summary>
    public class MainWindowVM : ViewModelBase
    {
        private ObservableCollection<ContactVM> _contactVMs;
        private ContactVM _selectedContactVM;
        private string _selectedGroup;
        private ObservableCollection<ContactVM> _visibleContacts;

        /// <summary>
        /// ViewModel for main window.
        /// </summary>
        public MainWindowVM()
        {
            ContactService = new ContactService();

            ContactVMs = ContactService.GetContactVMs();

            GroupNames =
                new ObservableCollection<string>(ContactService.Groups.Select(it => it.Name)) { SystemGroupNames.ALL_CONTACTS };

            SelectedGroup = SystemGroupNames.ALL_CONTACTS;

            AddGroupCommand = new AddGroupCommand();
            AddContactToGroupCommand = new AddContactToGroupCommand();
            RemoveContactFromGroupCommand = new RemoveContactFromGroupCommand();
            EditContactCommand = new EditContactCommand();

            AddContactCommand = new AddContactCommand();

            DeleteContactCommand = new DeleteContactCommand();
            DeleteGroupCommand = new DeleteGroupCommand();
        }

        /// <summary>
        /// Command for adding contact.
        /// </summary>
        public AddContactCommand AddContactCommand { get; }

        /// <summary>
        /// Command for adding contact ro group.
        /// </summary>
        public AddContactToGroupCommand AddContactToGroupCommand { get; }

        /// <summary>
        /// Command for adding contact group.
        /// </summary>
        public AddGroupCommand AddGroupCommand { get; }

        /// <summary>
        /// Service for interacting with contacts.
        /// </summary>
        public ContactService ContactService { get; }

        /// <summary>
        /// List of Contact View-Models.
        /// </summary>
        public ObservableCollection<ContactVM> ContactVMs
        {
            get => _contactVMs;
            set
            {
                _contactVMs = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command for deleting contact.
        /// </summary>
        public DeleteContactCommand DeleteContactCommand { get; }

        /// <summary>
        /// Command for deleting contact group.
        /// </summary>
        public DeleteGroupCommand DeleteGroupCommand { get; }

        /// <summary>
        /// Command for editing contact.
        /// </summary>
        public EditContactCommand EditContactCommand { get; }

        /// <summary>
        /// Collection of group names.
        /// </summary>
        public ObservableCollection<string> GroupNames { get; }

        /// <summary>
        /// Command for removing contact from group.
        /// </summary>
        public RemoveContactFromGroupCommand RemoveContactFromGroupCommand { get; }

        /// <summary>
        /// Selected Contact.
        /// </summary>
        public ContactVM SelectedContactVM
        {
            get => _selectedContactVM;
            set
            {
                _selectedContactVM = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Selected group name.
        /// </summary>
        public string SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged();
                UpdateVisibleContacts();
            }
        }

        /// <summary>
        /// Contacts for displaying.
        /// </summary>
        public ObservableCollection<ContactVM> VisibleContacts
        {
            get => _visibleContacts;
            set
            {
                _visibleContacts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Update main window.
        /// </summary>
        public void Update()
        {
            UpdateVisibleContacts();
        }

        /// <summary>
        /// Update displaying contacts.
        /// </summary>
        private void UpdateVisibleContacts()
        {
            if (_selectedGroup == SystemGroupNames.ALL_CONTACTS)
            {
                VisibleContacts = new ObservableCollection<ContactVM>(ContactVMs);
            }
            else
            {
                var group = ContactService.Groups.FirstOrDefault(it => it.Name == _selectedGroup);
                if (group != null)
                    VisibleContacts =
                        new ObservableCollection<ContactVM>(
                            ContactVMs.Where(it => it.GroupList.Contains(group.ResourceName)));
                else
                    VisibleContacts = new ObservableCollection<ContactVM>();
            }
        }
    }
}