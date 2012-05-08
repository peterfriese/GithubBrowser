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
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using System.Linq;

namespace GithubBrowser.Service
{
    public class ApplicationNavigationService
    {

        private PhoneApplicationFrame _mainFrame;
        private Dictionary<string, string> _currentNavigationParameters;
        public event NavigatingCancelEventHandler Navigating;

        private void SaveNavigationParameters(Uri uri)
        {
            string uriString = uri.ToString();
            if (uriString.Contains('?'))
            {
                _currentNavigationParameters = uriString.Substring(uriString.IndexOf('?') + 1).Split('&').Select(element =>
                {
                    string[] values = element.Split('=');
                    return new KeyValuePair<string, string>(values[0], values[1]);
                }).ToDictionary(i => i.Key, i => i.Value);
            }
            else
            {
                _currentNavigationParameters = new Dictionary<string, string>();
            }
        }

        public string GetParameter(string key, string defaultValue = "")
        {
            string result = defaultValue;
            if (_currentNavigationParameters != null && _currentNavigationParameters.ContainsKey(key))
            {
                result = _currentNavigationParameters[key];
            }
            return result;
        }

        public void NavigateTo(Uri pageUri)
        {
            if (EnsureMainFrame())
            {
                SaveNavigationParameters(pageUri);
                _mainFrame.Navigate(pageUri);
            }
        }

        public void NavigateTo(String uriString, params Object[] parameters)
        {
            Uri uri = new Uri(String.Format(uriString, parameters), UriKind.RelativeOrAbsolute);
            NavigateTo(uri);
        }

        public void GoBack()
        {
            if (EnsureMainFrame()
                && _mainFrame.CanGoBack)
            {
                _mainFrame.GoBack();
            }
        }

        private bool EnsureMainFrame()
        {
            if (_mainFrame != null)
            {
                return true;
            }

            _mainFrame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (_mainFrame != null)
            {
                // Could be null if the app runs inside a design tool
                _mainFrame.Navigating += (s, e) =>
                {
                    if (Navigating != null)
                    {
                        Navigating(s, e);
                    }
                };

                return true;
            }

            return false;
        }

    }
}
