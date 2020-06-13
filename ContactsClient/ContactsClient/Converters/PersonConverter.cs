namespace ContactsClient.Converters
{
    using System.Linq;

    using ContactsClient.Models;

    using Google.Apis.People.v1.Data;

    /// <summary>
    /// Mapper.
    /// </summary>
    public static class PersonConverter
    {
        public static Contact Convert(this Person person)
        {
            if (person == null)
                return null;

            var name = person.Names.FirstOrDefault();
            return new Contact
            {
                DisplayName = name?.DisplayName,
                FirstName = name?.FamilyName,
                LastName =  name?.GivenName,
                PhoneNumber = person.PhoneNumbers?.Select(it => it.CanonicalForm).ToList(),
                EmailAddress = person.EmailAddresses?.Select(it => it.Value).ToList()
            };
        }
    }
}