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

namespace GithubBrowser.ViewModel
{
    public class BasePivotViewModel: ViewModelBase
    {
        protected BaseViewModel CurrentViewModel { get; set; }

        public RelayCommand<SelectionChangedEventArgs> PivotChangedCommand { get; private set; }

        public RelayCommand RefreshCommand { get; private set; }

        public BasePivotViewModel()
        {
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
