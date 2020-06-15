namespace ContactsClient.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using ContactsClient.Commands;

    /// <summary>
    /// View-model of Contact.
    /// </summary>
    public class ContactVM : ViewModelBase
    {
        private string _firstName;
        private string _lastName;

        private ObservableCollection<PhoneNumberVM> _phoneNumbers;

        /// <summary>
        /// View-model of contact.
        /// </summary>
        public ContactVM()
        {
            PhoneNumbers = new ObservableCollection<PhoneNumberVM>();
            AddPhoneNumberCommand = new AddPhoneNumberCommand();
            GroupList = new List<string>();
        }

        /// <summary>
        /// Command for adding phone number.
        /// </summary>
        public AddPhoneNumberCommand AddPhoneNumberCommand { get; }

        /// <summary>
        /// Displayed name.
        /// </summary>
        public string DisplayName => $"{LastName} {FirstName}";

        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// List of group names.
        /// </summary>
        public List<string> GroupList { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Phone numbers.
        /// </summary>
        public ObservableCollection<PhoneNumberVM> PhoneNumbers
        {
            get => _phoneNumbers;
            set
            {
                _phoneNumbers = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Resource name - it's kind of ID.
        /// </summary>
        public string ResourceName { get; set; }
    }
}