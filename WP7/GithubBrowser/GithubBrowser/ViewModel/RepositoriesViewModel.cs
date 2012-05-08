using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GithubBrowser.Model;
using RestSharp;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Linq;
using GithubBrowser.Service;
using System.Windows;

namespace GithubBrowser.ViewModel
{
    public class RepositoriesViewModel : BaseViewModel
    {

        public RepositoriesViewModel(ApplicationNavigationService navigationService): base(navigationService)
        {
        }

        protected override void LoadSampleData()
        {
            Repositories = new ObservableCollection<Repository>()
            {
                new Repository() {Name="Applause", Description="A DSL for creating cross-platform mobile applications"},
                new Repository() {Name="node", Description="Awesome web platform"},
                new Repository() {Name="RestSharp", Description="REST for .NET"}
            };
        }

        protected override void LoadData()
        {
            BeginLoading();

            var client = new RestClient();
            client.BaseUrl = "https://api.github.com";
            var request = new RestRequest();

            if (ApplicationNavigationService != null)
            {
                string user = ApplicationNavigationService.GetParameter("user", "peterfriese");
                request.Resource = String.Format("users/{0}/repos", user);
                client.ExecuteAsync<List<Repository>>(request, response =>
                {
                    if (response.ResponseStatus == ResponseStatus.Completed)
                    {
                        var sortedRepos = from repo in response.Data
                                          orderby repo.Name
                                          select repo;

                        Repositories = new ObservableCollection<Repository>(sortedRepos);
                    }
                    DoneLoading();
                });
            }
        }

        private ObservableCollection<Repository> _repositories;
        public ObservableCollection<Repository> Repositories {
            get
            {
                if (_repositories == null)
                {
                    LoadData();
                }
                return _repositories;
            }
            set
            {
                if (_repositories != value)
                {
                    _repositories = value;
                    RaisePropertyChanged("Repositories");
                }
            }
        }

        private RelayCommand<Repository> _repositorySelectedCommand;
        public RelayCommand<Repository> RepositorySelectedCommand
        {
            get
            {
                return _repositorySelectedCommand ?? (_repositorySelectedCommand = new RelayCommand<Repository>(repository => {
                    if (repository != null)
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            ApplicationNavigationService.NavigateTo("/View/RepositoryView.xaml?user={0}&repository={1}", repository.Owner.Login, repository.Name);
                        });
                    }
                }));
            }
        }

    }
}