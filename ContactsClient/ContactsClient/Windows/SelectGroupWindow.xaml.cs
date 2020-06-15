namespace ContactsClient.Windows
{
    using System.Windows;

    /// <summary>
    /// Логика взаимодействия для SelectGroupWindow.xaml
    /// </summary>
    public partial class SelectGroupWindow : Window
    {
        public SelectGroupWindow()
        {
            InitializeComponent();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}