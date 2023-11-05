using System.Windows;
using System.Windows.Controls;
using static OAuthVK.ViewModel;

namespace OAuthVK
{
    public partial class APIPage : Page
    {
        public APIPage()
        {
            InitializeComponent();
        }

        private void ButtonGetUserData_Click(object sender, RoutedEventArgs e)
        {
            DataTextBox.Text = GetUserData();
        }

        private void ButtonGetAccountData_Click(object sender, RoutedEventArgs e)
        {

            DataTextBox.Text = GetAccountData();
        }

        private void ButtonGetBannedData_Click(object sender, RoutedEventArgs e)
        {
            DataTextBox.Text = GetBannedData();
        }
    }
}