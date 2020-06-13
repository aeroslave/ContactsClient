namespace ContactsClient.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    using ContactsClient.ContactService;
    using ContactsClient.Converters;
    using ContactsClient.Models;

    using Google.Apis.People.v1.Data;

    /// <summary>
    /// ViewModel for main window.
    /// </summary>
    public class MainWindowVM : ViewModelBase
    {
        private ObservableCollection<Contact> _contacts;
        private Contact _selectedContact;

        /// <summary>
        /// ViewModel for main window.
        /// </summary>
        public MainWindowVM()
        {
            ContactService = new ContactService();
            Persons = ContactService.Persons;

            Contacts = new ObservableCollection<Contact>(Persons.Select(it => it.Convert()));

            Contacts.CollectionChanged += ContactsOnCollectionChanged;
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

        /// <summary>
        /// List of Person.
        /// </summary>
        public List<Person> Persons { get; set; }

        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }

        private void ContactsOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
        }
    }
}