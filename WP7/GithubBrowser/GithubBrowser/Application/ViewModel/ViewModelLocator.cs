using Microsoft.Phone.Shell;
using System.Windows;
using Microsoft.Phone.Controls;
using GithubBrowser.Service;

namespace GithubBrowser.ViewModel
{
    public class ViewModelLocator
    {

        private static ApplicationNavigationService _applicationNavigationService;
        public static ApplicationNavigationService ApplicationNavigationService
        {
            get
            {
                if (_applicationNavigationService == null) 
                {
                    _applicationNavigationService = new ApplicationNavigationService();
                }
                return _applicationNavigationService;
            }
        }

        private static GithubService _githubService;
        public static GithubService GithubService
        {
            get
            {
                if (_githubService == null)
                {
                    _githubService = new GithubService();
                }
                return _githubService;
            }
        }

        private static HomeViewModel _homeViewModel;
        public static HomeViewModel HomeViewModel
        {
            get
            {
                if (_homeViewModel == null)
                {
                    _homeViewModel = new HomeViewModel(ApplicationNavigationService, GithubService);
                }
                return _homeViewModel;
            }
        }

        public RepositoriesViewModel RepositoriesViewModel
        {
            get
            {
                return new RepositoriesViewModel(ApplicationNavigationService, GithubService);
            }
        }

        public RepositoryViewModel RepositoryViewModel
        {
            get
            {
                return new RepositoryViewModel(ApplicationNavigationService, GithubService);
            }
        }

        public UsersViewModel FollowingUsersViewModel
        {
            get
            {
                return new FollowingUsersViewModel(ApplicationNavigationService, GithubService);
            }
        }

        public UsersViewModel FollowersUsersViewModel
        {
            get
            {
                return new FollowersUsersViewModel(ApplicationNavigationService, GithubService);
            }
        }

        public WelcomeViewModel WelcomeViewModel
        {
            get
            {
                return new WelcomeViewModel(ApplicationNavigationService, GithubService);
            }
        }

        public AuthenticationViewModel SignInViewModel
        {
            get
            {
                return new AuthenticationViewModel(ApplicationNavigationService, GithubService);
            }
        }

    }
}