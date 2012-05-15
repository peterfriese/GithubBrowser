using Microsoft.Phone.Controls;
using GithubBrowser.Extensions;

namespace GithubBrowser.View
{
    public partial class HomeView : PhoneApplicationPage
    {
        public HomeView()
        {
            InitializeComponent();
            this.ShowSplash();
            // this.Login();
        }
    }
}