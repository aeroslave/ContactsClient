namespace ContactsClient.ContactService
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Windows;

    using ContactsClient.Constants;
    using ContactsClient.Converters;
    using ContactsClient.ViewModels;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.PeopleService.v1;
    using Google.Apis.PeopleService.v1.Data;
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
            CreateService();

            GetPersons();
            GetGroups();
        }

        /// <summary>
        /// Create people service.
        /// </summary>
        private void CreateService()
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

            // Create the service.
            PeopleService = new PeopleServiceService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "ContactsClient2"
            });
        }

        /// <summary>
        /// List of contact groups.
        /// </summary>
        public List<ContactGroup> Groups { get; private set; }

        /// <summary>
        /// Service for interacting with Peolpe API.
        /// </summary>
        private PeopleServiceService PeopleService { get; set; }

        /// <summary>
        /// List of persons.
        /// </summary>
        private List<Person> Persons { get; set; }

        /// <summary>
        /// Create group.
        /// </summary>
        /// <param name="groupName">Name of group.</param>
        public void CreateGroup(string groupName)
        {
            var contactGroup = new ContactGroup { Name = groupName };
            var contactGroupRequest = new CreateContactGroupRequest { ContactGroup = contactGroup };

            var request = PeopleService.ContactGroups.Create(contactGroupRequest);

            try
            {
                request.Execute();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            GetGroups();
        }

        /// <summary>
        /// Create person from contact.
        /// </summary>
        /// <param name="contactVM">Contact.</param>
        public void CreatePerson(ContactVM contactVM)
        {
            var newPerson = new Person();
            FillPersonData(contactVM, newPerson);

            var request = PeopleService.People.CreateContact(newPerson);

            try
            {
                var createdPerson = request.Execute();
                contactVM.ResourceName = createdPerson.ResourceName;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            GetPersons();
        }

        /// <summary>
        /// Delete contact.
        /// </summary>
        /// <param name="contactVM">View-model of contact.</param>
        public void DeleteContact(ContactVM contactVM)
        {
            var request = PeopleService.People.DeleteContact(contactVM.ResourceName);

            try
            {
                request.Execute();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            GetPersons();
        }

        /// <summary>
        /// Delete group.
        /// </summary>
        /// <param name="groupName">Name of group.</param>
        public void DeleteGroup(string groupName)
        {
            var group = Groups.FirstOrDefault(it => it.Name == groupName);
            if (group == null)
                return;

            var request = PeopleService.ContactGroups.Delete(group.ResourceName);
            request.DeleteContacts = false;

            try
            {
                request.Execute();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Get contacts VM.
        /// </summary>
        /// <returns>Collection of contact view-models.</returns>
        public ObservableCollection<ContactVM> GetContactVMs()
        {
            return new ObservableCollection<ContactVM>(Persons.Select(it => it.Convert()));
        }

        /// <summary>
        /// Add or remove contact from group.
        /// </summary>
        /// <param name="contactVM">View-model of contact.</param>
        /// <param name="groupName">Name of group.</param>
        /// <param name="needRemove">Remove flag.</param>
        public void ModifyContactGroup(ContactVM contactVM, string groupName, bool needRemove)
        {
            var group = Groups.FirstOrDefault(it => it.Name == groupName);

            var person = Persons.FirstOrDefault(it => it.ResourceName == contactVM.ResourceName);

            if (group == null || person == null)
                return;

            var modifyMembersRequest = new ModifyContactGroupMembersRequest();

            if (needRemove)
                modifyMembersRequest.ResourceNamesToRemove = new List<string> { person.ResourceName };
            else
                modifyMembersRequest.ResourceNamesToAdd = new List<string> { person.ResourceName };

            var request = PeopleService.ContactGroups.Members.Modify(modifyMembersRequest, group.ResourceName);

            try
            {
                request.Execute();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            if (needRemove)
                contactVM.GroupList.Remove(group.ResourceName);
            else
                contactVM.GroupList.Add(group.ResourceName);

            GetGroups();
        }

        /// <summary>
        /// Update contact.
        /// </summary>
        /// <param name="contactVM">View-model of contact.</param>
        public void UpdateContact(ContactVM contactVM)
        {
            var person = Persons.FirstOrDefault(it => it.ResourceName == contactVM.ResourceName);

            if (person == null)
                return;

            FillPersonData(contactVM, person);

            var request = PeopleService.People.UpdateContact(person, person.ResourceName);
            request.UpdatePersonFields = "names,phoneNumbers,memberships";

            try
            {
                request.Execute();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            GetPersons();
        }

        /// <summary>
        /// Fill person data from contact view-model.
        /// </summary>
        /// <param name="contactVM">Contact view-model.</param>
        /// <param name="person">Information about person.</param>
        private static void FillPersonData(ContactVM contactVM, Person person)
        {
            var names = new List<Name>();
            var name = new Name
            {
                DisplayName = contactVM.DisplayName,
                FamilyName = contactVM.LastName,
                GivenName = contactVM.FirstName
            };

            names.Add(name);
            var phoneNumbers = contactVM.PhoneNumbers?
                .Select(phoneNumber => new PhoneNumber { Value = phoneNumber.Value })
                .ToList();

            person.Names = names;
            person.PhoneNumbers = phoneNumbers;
        }

        /// <summary>
        /// Get contact group.
        /// </summary>
        private void GetGroups()
        {
            var listRequest = PeopleService.ContactGroups.List();

            try
            {
                var groupsResponse = listRequest.Execute();
                var groups = groupsResponse.ContactGroups.ToList();

                Groups = groups.Where(it => !IsSystemGroup(it.Name)).ToList();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Fill person list.
        /// </summary>
        private void GetPersons()
        {
            var peopleRequest = PeopleService.People.Connections.List("people/me");

            peopleRequest.RequestMaskIncludeField =
                "person.names,person.phoneNumbers,person.memberships";

            var connectionsResponse = peopleRequest.Execute();
            Persons = connectionsResponse.Connections.ToList();
        }

        /// <summary>
        /// Check if it system group.
        /// </summary>
        /// <param name="groupName">Group name</param>
        /// <returns>True - if it is.</returns>
        private static bool IsSystemGroup(string groupName)
        {
            switch (groupName)
            {
                case SystemGroupNames.ALL:
                case SystemGroupNames.MY_CONTACTS:
                case SystemGroupNames.BLOCKED:
                case SystemGroupNames.CHAT:
                case SystemGroupNames.COWORKERS:
                case SystemGroupNames.FAMILY:
                case SystemGroupNames.FRIENDS:
                case SystemGroupNames.STARRED:
                    return true;

                default:
                    return false;
            }
        }
    }
}