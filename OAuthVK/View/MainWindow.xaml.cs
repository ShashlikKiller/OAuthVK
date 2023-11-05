using System.Windows;
using CefSharp.Wpf;
using CefSharp;
using System.Web;
using System;
using static OAuthVK.ViewModel;
using System.Web.UI;
using Newtonsoft.Json;

namespace OAuthVK
{
    public partial class MainWindow : Window
    {

        private ChromiumWebBrowser chromeBrowser;
        private WelcomePage WelcomePage = new WelcomePage();
        private APIPage ApiPage = new APIPage();

        public MainWindow()
        {
            InitializeComponent();
            BrowserPannel.Content = WelcomePage;
        }

        private void BrowserInitialize(string UriStr)
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            this.chromeBrowser = new ChromiumWebBrowser(UriStr);
            this.BrowserPannel.Content = chromeBrowser;
            chromeBrowser.AddressChanged += BrowserAdressChanged;
        }

        private void Auth_btn_Click(object sender, RoutedEventArgs e)
        {
            BrowserInitialize(GetUriStr());
            //Auth_btn.IsEnabled = false;
            Auth_btn.Visibility = Visibility.Hidden;

        }

        private void BrowserAdressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var uri = new Uri((string)e.NewValue);
            if (uri.AbsoluteUri.Contains(@"oauth.vk.com/blank.html"))
            {
                string url = uri.Fragment;
                url = url.Trim('#');
                var _access_token = HttpUtility.ParseQueryString(url).Get("access_token");
                var _userID = HttpUtility.ParseQueryString(url).Get("user_id");
                GetUserConfInf(_access_token, _userID);
                if (_access_token!=null) BrowserPannel.Content = ApiPage;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
