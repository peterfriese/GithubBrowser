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
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;

namespace GithubBrowser.ViewModel
{
    public class SignInViewModel: BaseViewModel
    {

        public SignInViewModel(ApplicationNavigationService navigationService, BaseRestService restService)
            : base(navigationService, restService)
        {
            Messenger.Default.Register<AuthenticationMessage>(RestService, (msg) => {
                if (msg.LoggedIn)
                {
                    RaisePropertyChanged("IsLoggedIn", false, msg.LoggedIn, true);
                }
            });
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

        public const string LoginPropertyName = "Login";
        private string _login = null;
        public string Login
        {
            get
            {
                return _login;
            }

            set
            {
                if (_login == value)
                {
                    return;
                }

                var oldValue = _login;
                _login = value;

                RaisePropertyChanged(LoginPropertyName);
            }
        }

        public const string PasswordPropertyName = "Password";
        private string _password = null;
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                if (_password == value)
                {
                    return;
                }

                var oldValue = _password;
                _password = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(PasswordPropertyName);
            }
        }

        private RelayCommand _signInCommand;
        public RelayCommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand(() =>
                {
                    RestService.Authenticate(Login, Password);
                }));
            }
        }
    }
}

