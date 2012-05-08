using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows;
using GithubBrowser.Extensions;

namespace GithubBrowser.View
{
    public partial class RepositoriesView : PhoneApplicationPage
    {
        public RepositoriesView()
        {
            InitializeComponent();
            this.ShowSplash();
        }
    }
}