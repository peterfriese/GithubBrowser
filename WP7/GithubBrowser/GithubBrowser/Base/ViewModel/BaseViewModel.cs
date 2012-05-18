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
using GalaSoft.MvvmLight;
using GithubBrowser.Service;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;

namespace GithubBrowser.ViewModel
{
    public abstract class BaseViewModel : ViewModelBase
    {
        public BaseViewModel(BaseViewModel parentViewModel, ApplicationNavigationService navigationService, BaseRestService restService)
        {
            ParentViewModel = parentViewModel;
            ApplicationNavigationService = navigationService;
            RestService = restService;
            if (IsInDesignMode)
            {
                LoadSampleData();
            }
            else
            {
                if (RestService.IsAuthenticated)
                {
                    Refresh();
                }
            }

            Messenger.Default.Register<AuthenticationMessage>(RestService, (msg) =>
            {
                if (msg.LoggedIn)
                {
                    Refresh();
                }
            });

        }

        public BaseViewModel(ApplicationNavigationService navigationService, BaseRestService restService): this(null, navigationService, restService)
        {
        }

        protected BaseViewModel ParentViewModel { get; private set; }
        protected bool HasParent 
        {
            get
            {
                return ParentViewModel != null;
            }
        }

        protected ApplicationNavigationService ApplicationNavigationService { get; set; }
        protected BaseRestService RestService { get; set; }

        private int _loadingParties = 0;
        public bool Loading
        {
            get
            {
                if (HasParent)
                {
                    return ParentViewModel.Loading;
                }
                else
                {
                    return _loadingParties > 0;
                }
            }
            private set
            {
                if (HasParent)
                {
                    ParentViewModel.Loading = value;
                }
                else
                {
                    if (value)
                    {
                        _loadingParties++;
                    }
                    else
                    {
                        _loadingParties--;
                    }
                    Messenger.Default.Send<ProgressMessage>(new ProgressMessage(_loadingParties));
                }
                // RaisePropertyChanged("Loading");
            }
        }

        protected void BeginLoading()
        {
            //Deployment.Current.Dispatcher.BeginInvoke(() =>
            //{
                Loading = true;
            //});
        }

        protected void DoneLoading()
        {
            //Deployment.Current.Dispatcher.BeginInvoke(() =>
            //{
                Loading = false;
            //});
        }

        protected abstract void LoadSampleData();
        protected abstract void LoadData();

        public void Refresh()
        {
            LoadData();
        }

    }
}
