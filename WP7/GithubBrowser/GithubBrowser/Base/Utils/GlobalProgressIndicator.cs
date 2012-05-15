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
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using GithubBrowser.ViewModel;

namespace GithubBrowser.Utils
{
    public class GlobalProgressIndicator
    {
        private GlobalProgressIndicator()
        {
        }

        private static GlobalProgressIndicator _default;
        public static GlobalProgressIndicator Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new GlobalProgressIndicator();
                    //var rootFrame = App.Current.RootVisual as PhoneApplicationFrame;
                    //if (rootFrame != null)
                    //{
                    //    _default.Initialize(rootFrame);
                    //}
                }
                return _default;
            }
        }

        public void Initialize(PhoneApplicationFrame frame)
        {
            frame.Navigated += OnRootFrameNavigated;

            Messenger.Default.Register<ProgressMessage>(this, msg =>
            {
                if (msg.IsActive)
                {
                    ProgressIndicator.IsIndeterminate = true;
                    ProgressIndicator.IsVisible = true;
                }
                else
                {
                    ProgressIndicator.IsVisible = false;
                }
            });
        }

        private void OnRootFrameNavigated(object sender, NavigationEventArgs args)
        {
            var content = args.Content;
            var page = content as PhoneApplicationPage;
            if (page != null)
            {
                SystemTray.SetProgressIndicator(page, ProgressIndicator);
            }
        }

        protected ProgressIndicator ProgressIndicator
        {
            get
            {
                var progressIndicator = SystemTray.ProgressIndicator;
                if (progressIndicator == null)
                {
                    progressIndicator = new ProgressIndicator();
                    SystemTray.ProgressIndicator = progressIndicator;
                }
                return progressIndicator;
            }
        }


    }
}
