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
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;

namespace GithubBrowser.ViewModel
{
    public class WelcomeViewModel: BaseViewModel
    {

        public WelcomeViewModel(ApplicationNavigationService navigationService, BaseRestService restService)
            : base(navigationService, restService)
        {
        }

        protected override void LoadSampleData()
        {
        }

        protected override void LoadData()
        {
        }

        public bool IsLoggedIn
        {
            get 
            {
                return RestService.IsAuthenticated;
            }

        }

        private RelayCommand _signInCommand;
        public RelayCommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand(() =>
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        ApplicationNavigationService.NavigateTo("/Application/View/SignInView.xaml");
                    });
                }));
            }
        }
    }
}
