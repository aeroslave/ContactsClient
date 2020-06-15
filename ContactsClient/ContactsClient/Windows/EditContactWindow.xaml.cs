namespace ContactsClient.Windows
{
    using System.Windows;

    /// <summary>
    /// Логика взаимодействия для EditContactWindow.xaml
    /// </summary>
    public partial class EditContactWindow : Window
    {
        public EditContactWindow()
        {
            InitializeComponent();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}