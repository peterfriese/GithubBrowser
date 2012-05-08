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
using GithubBrowser.Model;
using RestSharp;
using System.Collections.Generic;

namespace GithubBrowser.ViewModel
{
    public class CommitsViewModel: BaseViewModel
    {

        public CommitsViewModel(RepositoryViewModel repositoryViewModel, ApplicationNavigationService navigationService): base(repositoryViewModel, navigationService)
        {
        }

        protected override void LoadSampleData()
        {
            Commits = new ObservableCollection<CommitWrapper>() {
                new CommitWrapper() { Url="http://testurl", Commit = new Commit { Message = "Just a test", Committer = { Name = "Peter Friese", Email = "peter@peterfriese.de" } } },
                new CommitWrapper() { Url="http://testurl", Commit = new Commit { Message = "Another test", Committer = { Name = "Peter Friese", Email = "peter@peterfriese.de" } } },
                new CommitWrapper() { Url="http://testurl", Commit = new Commit { Message = "Yet another test", Committer = { Name = "Peter Friese", Email = "peter@peterfriese.de" } } }
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
                string repository = ApplicationNavigationService.GetParameter("repository");
                request.Resource = String.Format("repos/{0}/{1}/commits", user, repository);
                client.ExecuteAsync<List<CommitWrapper>>(request, response =>
                {
                    if (response.ResponseStatus == ResponseStatus.Completed)
                    {
                        Commits = new ObservableCollection<CommitWrapper>(response.Data);
                    }
                    DoneLoading();
                });
            }
        }

        private ObservableCollection<CommitWrapper> _commits;
        public ObservableCollection<CommitWrapper> Commits
        {
            get
            {
                if (_commits == null)
                {
                    LoadData();
                }
                return _commits;
            }

            set
            {
                if (_commits != value)
                {
                    _commits = value;
                    RaisePropertyChanged("Commits");
                }
            }
        }
    }
}
