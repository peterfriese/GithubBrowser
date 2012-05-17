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
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls;
using GithubBrowser.Service;

namespace GithubBrowser.ViewModel
{
    public class BasePivotViewModel : AuthenticationViewModel
    {
        protected BaseViewModel CurrentViewModel { get; set; }

        // this command will be invoked when the user changes from one pivot element to another one
        public RelayCommand<SelectionChangedEventArgs> PivotChangedCommand { get; private set; }

        public RelayCommand RefreshCommand { get; private set; }

        public BasePivotViewModel(ApplicationNavigationService navigationService, BaseRestService restService): base(navigationService, restService)
        {

            // when the user changes from one pivot item to another one, we take note of the new viewmodel
            // so we can invoke commands on it (e.g. refresh)
            PivotChangedCommand = new RelayCommand<SelectionChangedEventArgs>(action =>
            {
                if (action != null)
                {
                    PivotItem pivotItem = action.AddedItems[0] as PivotItem;
                    if (pivotItem != null)
                    {
                        CurrentViewModel = pivotItem.DataContext as BaseViewModel;
                    }
                }
            });

            RefreshCommand = new RelayCommand(() =>
            {
                RefreshCurrentViewModel();
            });
        }

        protected void RefreshCurrentViewModel()
        {
            if (CurrentViewModel != null)
            {
                CurrentViewModel.Refresh();
            }
        }
    }
}
