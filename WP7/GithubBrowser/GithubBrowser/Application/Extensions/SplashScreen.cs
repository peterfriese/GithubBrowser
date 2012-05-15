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
using GithubBrowser.View;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Threading;

namespace GithubBrowser.Extensions
{
    public static class SplashScreen
    {

        public static void ShowSplash(this PhoneApplicationPage page)
        {
            Popup popup = new Popup() 
            { 
                IsOpen = true, 
                Child = new UserControls.SplashScreen() 
            };

            BackgroundWorker backroundWorker = new BackgroundWorker();
            RunBackgroundWorker(backroundWorker, popup);
        }

        private static void RunBackgroundWorker(BackgroundWorker backroundWorker, Popup popup)
        {
            backroundWorker.DoWork += ((s, args) =>
            {
                Thread.Sleep(1000);
            });

            backroundWorker.RunWorkerCompleted += ((s, args) =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    CreateFadeInOutAnimation(popup, 1.0, 0.0).Begin();
                }
            );
            });
            backroundWorker.RunWorkerAsync();
        }

        private static Storyboard CreateFadeInOutAnimation(Popup popup, double from, double to)
        {
            Storyboard sb = new Storyboard();

            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = from;
            fadeInAnimation.To = to;
            fadeInAnimation.Duration = new Duration(TimeSpan.FromSeconds(1.0));
            Storyboard.SetTarget(fadeInAnimation, popup.Child);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));
            sb.Children.Add(fadeInAnimation);

            sb.Completed += (sender, eventarg) =>
            {
                popup.IsOpen = false;
            };
            return sb;
        }

    }
}
