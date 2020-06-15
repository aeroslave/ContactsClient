namespace ContactsClient.ViewModels
{
    /// <summary>
    /// Phone Number view-model.
    /// </summary>
    public class PhoneNumberVM : ViewModelBase
    {
        private string _value;

        /// <summary>
        /// Phone number value.
        /// </summary>
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
    }
}