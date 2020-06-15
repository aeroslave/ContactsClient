namespace ContactsClient.Converters
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using ContactsClient.ViewModels;

    using Google.Apis.PeopleService.v1.Data;

    /// <summary>
    /// Mapper.
    /// </summary>
    public static class PersonConverter
    {
        /// <summary>
        /// Converting person in contact view-model.
        /// </summary>
        /// <param name="person">Model of person.</param>
        /// <returns>Contact View-model.</returns>
        public static ContactVM Convert(this Person person)
        {
            if (person == null)
                return null;

            var personPhoneNumbers = person.PhoneNumbers?.Select(it => it.Value).ToList();
            var name = person.Names?.FirstOrDefault();

            var contact = new ContactVM
            {
                FirstName = name?.FamilyName,
                LastName = name?.GivenName,
                GroupList =
                    person.Memberships.Select(it => it.ContactGroupMembership.ContactGroupResourceName).ToList(),
                ResourceName = person.ResourceName
            };

            if (personPhoneNumbers?.Any() == true)
                contact.PhoneNumbers =
                    new ObservableCollection<PhoneNumberVM>(personPhoneNumbers.Select(it =>
                        new PhoneNumberVM { Value = it }));

            return contact;
        }
    }
}