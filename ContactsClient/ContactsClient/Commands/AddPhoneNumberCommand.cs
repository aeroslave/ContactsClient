namespace ContactsClient.Commands
{
    using ContactsClient.ViewModels;

    /// <summary>
    /// Command for adding phone number.
    /// </summary>
    public class AddPhoneNumberCommand : BaseTypedCommand<ContactVM>
    {
        /// <summary>
        /// Executing command.
        /// </summary>
        /// <param name="contactVM">View-model of contact.</param>
        protected override void Execute(ContactVM contactVM)
        {
            contactVM.PhoneNumbers.Add(new PhoneNumberVM { Value = string.Empty });
        }
    }
}