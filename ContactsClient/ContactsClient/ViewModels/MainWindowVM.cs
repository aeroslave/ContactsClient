namespace ContactsClient.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows.Input;

    using ContactsClient.Commands;
    using ContactsClient.ContactService;
    using ContactsClient.Converters;
    using ContactsClient.Models;

    using Google.Apis.PeopleService.v1.Data;

    /// <summary>
    /// ViewModel for main window.
    /// </summary>
    public class MainWindowVM : ViewModelBase
    {
        private ObservableCollection<Contact> _contacts;
        private Contact _selectedContact;
        private string _selectedGroup;
        private ObservableCollection<Contact> _visibleContacts;

        public ICommand AddGroupCommand { get; set; }
        public ICommand DeleteGroupCommand { get; set; }
        public ICommand AddContactCommand { get; set; }
        public ICommand DeleteContactCommand { get; set; }

        /// <summary>
        /// ViewModel for main window.
        /// </summary>
        public MainWindowVM()
        {
            ContactService = new ContactService();
            Persons = ContactService.Persons;

            Contacts = new ObservableCollection<Contact>(Persons.Select(it => it.Convert()));

            GroupNames =
                new ObservableCollection<string>(ContactService.Groups.Select(it => it.Name)) { "Все контакты" };

            SelectedGroup = "Все контакты";

            AddGroupCommand = new AddGroupCommand();
        }

        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Service for interacting with contacts.
        /// </summary>
        public ContactService ContactService { get; }

        public ObservableCollection<string> GroupNames { get; set; }

        /// <summary>
        /// List of Person.
        /// </summary>
        public List<Person> Persons { get; set; }

        /// <summary>
        /// Selected Contact.
        /// </summary>
        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }

        public string SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged();
                if (_selectedGroup == "Все контакты")
                {
                    VisibleContacts = Contacts;
                }
                else
                {
                    var group = ContactService.Groups.FirstOrDefault(it => it.Name == _selectedGroup);
                    if (group != null)
                        VisibleContacts =
                            new ObservableCollection<Contact>(
                                Contacts.Where(it => it.GroupList.Contains(group.ResourceName)));
                }
            }
        }

        public ObservableCollection<Contact> VisibleContacts
        {
            get { return _visibleContacts; }
            set
            {
                _visibleContacts = value; 
                OnPropertyChanged();
            }
        }
    }
}