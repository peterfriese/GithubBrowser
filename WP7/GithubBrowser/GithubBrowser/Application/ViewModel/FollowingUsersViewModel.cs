using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GithubBrowser.Service;

namespace GithubBrowser.ViewModel
{
    public class FollowingUsersViewModel: UsersViewModel
    {

        public FollowingUsersViewModel(ApplicationNavigationService navigationService, BaseRestService restService)
            : base(navigationService, restService)
        {
        }

        protected override string ResourceURI
        {
            get
            {
                return "users/{0}/following";
            }
        }

    }
}
