namespace ContactsClient.ContactService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.People.v1;
    using Google.Apis.People.v1.Data;
    using Google.Apis.Services;

    /// <summary>
    /// Service for interacting with contacts.
    /// </summary>
    public class ContactService
    {
        /// <summary>
        /// Service for interacting with contacts.
        /// </summary>
        public ContactService()
        {
            // Create OAuth credential.
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "812699070892-jfhd9k78d684f84dvbanack0dhrppg2s.apps.googleusercontent.com",
                    ClientSecret = "Dta7I5P72ZFQMMSzR7DRn7Ks"
                },
                new[] { "profile", "https://www.googleapis.com/auth/contacts.readonly" },
                "me",
                CancellationToken.None).Result;

            // Create the service.
            var peopleService = new PeopleService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "ContactsClient"
            });

            var peopleRequest = peopleService.People.Connections.List("people/me");
            peopleRequest.RequestMaskIncludeField = "person.names,person.emailAddresses,person.phoneNumbers,person.addresses";
            var connectionsResponse = peopleRequest.Execute();
            Persons = connectionsResponse.Connections.ToList();
        }

        /// <summary>
        /// List of persons
        /// </summary>
        public List<Person> Persons { get; set; }
    }
}