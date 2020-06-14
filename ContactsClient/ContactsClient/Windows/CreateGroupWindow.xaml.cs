namespace ContactsClient.Windows
{
    using System.Windows;

    /// <summary>
    /// Логика взаимодействия для CreateGroupWindow.xaml
    /// </summary>
    public partial class CreateGroupWindow : Window
    {
        public CreateGroupWindow()
        {
            InitializeComponent();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}