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
using System.Collections.ObjectModel;
using RestSharp;
using System.Collections.Generic;
using GithubBrowser.Model;
using System.Linq;
using GalaSoft.MvvmLight.Command;

namespace GithubBrowser.ViewModel
{
    public abstract class UsersViewModel: BaseViewModel
    {

        public UsersViewModel(ApplicationNavigationService navigationService, BaseRestService restService)
            : base(navigationService, restService)
        {
        }

        protected override void LoadSampleData()
        {
            Users = new ObservableCollection<User>()
            {
                new User() {Login="peterfriese", AvatarUrl="https://secure.gravatar.com/avatar/3b4865566313c84ac124ec691c1fdb98?d=https://a248.e.akamai.net/assets.github.com%2Fimages%2Fgravatars%2Fgravatar-140.png"},
                new User() {Login="mficner", AvatarUrl="https://secure.gravatar.com/avatar/ae6b20909c7b4e5ce084014598c99661?d=https://a248.e.akamai.net/assets.github.com%2Fimages%2Fgravatars%2Fgravatar-140.png"},
                new User() {Login="HBehrens", AvatarUrl="https://secure.gravatar.com/avatar/20b6dc14c336afea468505d038ce69d1?d=https://a248.e.akamai.net/assets.github.com%2Fimages%2Fgravatars%2Fgravatar-140.png"}
            };

        }

        protected abstract string ResourceURI
        {
            get;
        }

        protected override void LoadData()
        {
            BeginLoading();

            var client = RestService.Client;
            var request = new RestRequest();

            if (ApplicationNavigationService != null)
            {
                // use this if we navigate here using a URI: string user = ApplicationNavigationService.GetParameter("user", "applause");
                string user = RestService.Login;
                request.Resource = String.Format(ResourceURI, user);
                client.ExecuteAsync<List<User>>(request, response =>
                {
                    if (response.ResponseStatus == ResponseStatus.Completed)
                    {
                        var sortedUsers = from oneUser in response.Data
                                          orderby oneUser.Login
                                          select oneUser;

                        Users = new ObservableCollection<User>(sortedUsers);
                    }
                    DoneLoading();
                });
            }
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users {
            get
            {
                if (_users == null)
                {
                    LoadData();
                }
                return _users;
            }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    RaisePropertyChanged("Users");
                }
            }
        }

        private RelayCommand<User> _userSelectedCommand;
        public RelayCommand<User> UserSelectedCommand
        {
            get
            {
                return _userSelectedCommand ?? (_userSelectedCommand = new RelayCommand<User>(repository =>
                {
                    if (repository != null)
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            // ApplicationNavigationService.NavigateTo("/Application/View/RepositoryView.xaml?user={0}&repository={1}", repository.Owner.Login, repository.Name);
                        });
                    }
                }));
            }
        }

    }
}