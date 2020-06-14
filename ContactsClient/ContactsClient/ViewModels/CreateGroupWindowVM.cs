namespace ContactsClient.ViewModels
{
    public class CreateGroupWindowVM : ViewModelBase
    {
        private string _groupName;

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

        public bool IsDataValid => !string.IsNullOrWhiteSpace(_groupName);
    }
}