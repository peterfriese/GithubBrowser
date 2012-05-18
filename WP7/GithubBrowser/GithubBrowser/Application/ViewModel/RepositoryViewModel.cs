using GalaSoft.MvvmLight;
using RestSharp;
using System.Collections.Generic;
using GithubBrowser.Model;
using GithubBrowser.Service;
using System;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace GithubBrowser.ViewModel
{
    public class RepositoryViewModel : BaseViewModel
    {

        public RepositoryViewModel(ApplicationNavigationService navigationService, BaseRestService restService): base(navigationService, restService)
        {
            // TODO: there has got to be a simpler way to do this!
            Messenger.Default.Register<PropertyChangedMessage<Repository>>(this, action =>
            {
                RaisePropertyChanged("ForkedFrom");
            });
        }

        protected override void LoadSampleData()
        {
            Repository = new Repository()
            {
                Name = "Applause",
                Description = "Cross platform mobile development toolkit consisting of a DSL for defining mobile apps and code generators for creating native apps for iOS, Android, Windows Phone 7 and Google App Engine. Based on Eclipse and Xtext. the official repo is at applause/applause.",
                Owner = new User() {
                    Login = "peterfriese",
                    Name = "Peter Friese"
                }
            };
        }

        protected override void LoadData()
        {
            BeginLoading();

            var client = RestService.Client;
            client.BaseUrl = "https://api.github.com";
            var request = new RestRequest();

            if (ApplicationNavigationService != null)
            {
                string user = ApplicationNavigationService.GetParameter("user");
                string repository = ApplicationNavigationService.GetParameter("repository");

                request.Resource = String.Format("repos/{0}/{1}", user, repository);
                client.ExecuteAsync<Repository>(request, response =>
                {
                    if (response.ResponseStatus == ResponseStatus.Completed)
                    {
                        var oldValue = Repository;
                        Repository = response.Data;
                        DoneLoading();
                    }
                });
            }

        }

        public const string RepositoryPropertyName = "Repository";
        private Repository _repository = null;

        public Repository Repository
        {
            get
            {
                return _repository;
            }

            set
            {
                if (_repository == value)
                {
                    return;
                }

                var oldValue = _repository;
                _repository = value;

                RaisePropertyChanged(RepositoryPropertyName, oldValue, value, true);
            }
        }

        public string ForkedFrom
        {
            get
            {
                if ( (Repository != null) && (Repository.Parent != null) && (Repository.Parent.Owner != null) )
                {
                    return String.Format("{0}/{1}", Repository.Parent.Owner.Login, Repository.Parent.Name);
                }
                return "";
            }
        }

        private CommitsViewModel _commitsViewModel;
        public CommitsViewModel CommitsViewModel
        {
            get
            {
                if (_commitsViewModel == null)
                {
                    _commitsViewModel = new CommitsViewModel(this, ApplicationNavigationService, RestService);
                    RaisePropertyChanged("CommitsViewModel");
                }
                return _commitsViewModel;
            }
        }

    }
}