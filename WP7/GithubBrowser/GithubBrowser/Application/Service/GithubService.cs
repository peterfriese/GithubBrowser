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
using RestSharp;

namespace GithubBrowser.Service
{
    public class GithubService: BaseRestService
    {

        protected override RestClient CreateClient()
        {
            var client = new RestClient("https://api.github.com");
            if ((Password != null) && (Login != null))
            {
                client.Authenticator = new HttpBasicAuthenticator(Login, Password);
            }
            return client;
        }
    }
}
