namespace ContactsClient.ViewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Base class for ViewModels.
    /// </summary>
    public abstract class ViewModelBase: INotifyPropertyChanged
    {
        /// <summary>
        /// Event that occurs when properties change
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// EventHandler.
        /// </summary>
        /// <param name="propertyName">PropertyName.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}