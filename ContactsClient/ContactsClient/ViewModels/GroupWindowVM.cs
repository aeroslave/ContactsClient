namespace ContactsClient.ViewModels
{
    /// <summary>
    /// Group view-model.
    /// </summary>
    public class GroupWindowVM : ViewModelBase
    {
        private string _groupName;

        /// <summary>
        /// Group name.
        /// </summary>
        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsDataValid));
            }
        }

        /// <summary>
        /// Data validity flag 
        /// </summary>
        public bool IsDataValid => !string.IsNullOrWhiteSpace(_groupName);
    }
}