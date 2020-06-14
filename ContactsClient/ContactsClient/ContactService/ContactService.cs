namespace ContactsClient.ContactService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using ContactsClient.Models;

    using Google.Apis.Auth.OAuth2;

    using Google.Apis.PeopleService.v1;
    using Google.Apis.PeopleService.v1.Data;

    using Google.Apis.Services;

    /// <summary>
    /// Service for interacting with contacts.
    /// </summary>
    public class ContactService
    {
        public UserCredential UserCredential { get; set; }
        /// <summary>
        /// Service for interacting with contacts.
        /// </summary>
        public ContactService()
        {
            // Create OAuth credential.
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "812699070892-9qfj0cpq2s76hgidon5rrjq90lernd19.apps.googleusercontent.com",
                    ClientSecret = "db4chSxaRCg-xvLERQspSFLV"
                },
                new[] { "profile", "https://www.googleapis.com/auth/contacts" },
                "admin",
                CancellationToken.None).Result;

            UserCredential = credential;

            // Create the service.
            PeopleService = new PeopleServiceService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "ContactsClient2"
            });

            var peopleRequest = PeopleService.People.Connections.List("people/me");

            peopleRequest.RequestMaskIncludeField = "person.names,person.emailAddresses,person.phoneNumbers,person.addresses,person.memberships";
            //peopleRequest.Fields = "names,emailAddresses,phoneNumbers,addresses";


            var connectionsResponse = peopleRequest.Execute();
            Persons = connectionsResponse.Connections.ToList();

            GetGroups();
        }

        public PeopleServiceService PeopleService { get; set; }

        /// <summary>
        /// List of persons
        /// </summary>
        public List<Person> Persons { get; set; }

        public List<ContactGroup> Groups { get; set; }

        /// <summary>
        /// Create person from contact.
        /// </summary>
        /// <param name="contact">Contact.</param>
        public void CreatePerson(Contact contact)
        {
            var newPerson = new Person();
            var names = new List<Name>();
            var name = new Name
            {
                DisplayName = contact.DisplayName,
                FamilyName = contact.LastName,
                GivenName = contact.FirstName
            };

            names.Add(name);
            var phoneNumbers = contact.PhoneNumbers
                .Select(phoneNumber => new PhoneNumber { CanonicalForm = phoneNumber })
                .ToList();

            var emailAddress = contact.EmailAddress
                .Select(email => new EmailAddress { Value = email }).ToList();

            newPerson.Names = names;
            newPerson.PhoneNumbers = phoneNumbers;
            newPerson.EmailAddresses = emailAddress;

            var request = PeopleService.People.CreateContact(newPerson);
            var createdPerson = request.Execute();
        }

        public void CreateGroup(string groupName)
        {
            var contactGroup = new ContactGroup { Name = groupName };
            var contactGroupRequest = new CreateContactGroupRequest { ContactGroup = contactGroup };
            
            var request = PeopleService.ContactGroups.Create(contactGroupRequest);
            request.Credential = UserCredential;
            
            GetGroups();
        }

        public void GetGroups()
        {
            var listRequest = PeopleService.ContactGroups.List();
            var groupsResponse = listRequest.Execute();
            var groups = groupsResponse.ContactGroups.ToList();

            Groups = groups.Where(it => it.Name != "myContacts"
            && it.Name != "all").ToList();
        }
    }
}