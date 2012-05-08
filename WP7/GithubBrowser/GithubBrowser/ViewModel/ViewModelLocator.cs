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

        public RepositoriesViewModel RepositoriesViewModel
        {
            get
            {
                return new RepositoriesViewModel(ApplicationNavigationService);
            }
        }

        public RepositoryViewModel RepositoryViewModel
        {
            get
            {
                return new RepositoryViewModel(ApplicationNavigationService);
            }
        }

    }
}