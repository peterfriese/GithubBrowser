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

namespace GithubBrowser.Model
{
    public class User
    {
        public string Type { get; set; }
        public bool Hireable { get; set; }
        public string Blog { get; set; }
        public string Bio { get; set; }
        public string CreatedAt { get; set; }
        public string Location { get; set; }
        public int Following { get; set; }
        public int Followers { get; set; }
        public string AvatarUrl { get; set; }
        public int PublicRepos { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string GravatarId { get; set; }
        public string Company { get; set; }
        public int Id { get; set; }
        public string Login { get; set; }
        public int PublicGists { get; set; }
        public string HtmlUrl { get; set; }
        public object Email { get; set; }
    }
}
