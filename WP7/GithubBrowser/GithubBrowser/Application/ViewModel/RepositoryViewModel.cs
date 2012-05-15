using GalaSoft.MvvmLight;
using RestSharp;
using System.Collections.Generic;
using GithubBrowser.Model;
using GithubBrowser.Service;
using System;
using System.ComponentModel;

namespace GithubBrowser.ViewModel
{
    public class RepositoryViewModel : BaseViewModel
    {

        public RepositoryViewModel(ApplicationNavigationService navigationService, BaseRestService restService): base(navigationService, restService)
        {
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
                        Repository = response.Data;
                        RaisePropertyChanged(RepositoryPropertyName);
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

                // Update bindings, no broadcast
                RaisePropertyChanged(RepositoryPropertyName);
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