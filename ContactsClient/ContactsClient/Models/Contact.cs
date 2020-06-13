﻿namespace ContactsClient.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Model of Contact.
    /// </summary>
    public class Contact
    {
        public string DisplayName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> PhoneNumber { get; set; }

        public List<string> EmailAddress { get; set; }
    }
}