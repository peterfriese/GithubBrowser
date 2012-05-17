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
using RestSharp;
using GithubBrowser.Model;
using System.IO.IsolatedStorage;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace GithubBrowser.Service
{
    public abstract class BaseRestService
    {

        public BaseRestService()
        {
            Init();
        }

        public string Login
        {
            get
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("Login"))
                {
                    var result = IsolatedStorageSettings.ApplicationSettings["Login"];
                    if (result == null)
                        return null;
                    else
                        return result.ToString();
                }
                return null;
            }

            set
            {
                IsolatedStorageSettings.ApplicationSettings["Login"] = value;
            }
        }

        public string Password
        {
            get
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("Password"))
                {
                    var result = IsolatedStorageSettings.ApplicationSettings["Password"];
                    if (result == null)
                        return null;
                    else
                        return result.ToString();
                }
                return null;
            }

            set
            {
                IsolatedStorageSettings.ApplicationSettings["Password"] = value;
            }
        }

        public void Init()
        {
            if ((Login != null) && (Password != null))
            {
                Authenticate(Login, Password);
            }
            else
            {
            }
        }

        public void Authenticate(string login, string password)
        {
            Login = login;
            Password = password;

            var client = this.Client;
            var request = new RestRequest("/user");
            client.ExecuteAsync<User>(request, response =>
            {
                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                }
                else 
                {
                    Login = null;
                    Password = null;
                }
                Messenger.Default.Send<AuthenticationMessage>(new AuthenticationMessage(_isAuthenticated));
            });
        }

        public void UnAuthenticate()
        {
            Login = null;
            Password = null;
            Messenger.Default.Send<AuthenticationMessage>(new AuthenticationMessage(_isAuthenticated));
        }

        private bool _isAuthenticated = true;
        public bool IsAuthenticated { 
            get 
            {
                return ((Password != null) && (Login != null));
            }
        }

        private RestClient _client;
        public RestClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = CreateClient();
                }
                return _client;
            }
        }

        protected abstract RestClient CreateClient();
    }
}
