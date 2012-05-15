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
using System.ComponentModel;

namespace GithubBrowser.Model
{
    public class Repository: INotifyPropertyChanged
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Owner { get; set; }
        public int Watchers { get; set; }
        public int Forks { get; set; }
        public int OpenIssues { get; set; }

        public bool HasDownloads { get; set; }
        public string SshUrl { get; set; }
        public string Language { get; set; }
        public string CreatedAt { get; set; }
        public bool HasWiki { get; set; }
        public bool Fork { get; set; }
        public string Homepage { get; set; }

        public string SvnUrl { get; set; }
        public string UpdatedAt { get; set; }
        public int Size { get; set; }
        public bool Private { get; set; }

        public string Git_Url { get; set; }

        public object MirrorUrl { get; set; }
        public string CloneUrl { get; set; }
        public string PushedAt { get; set; }
        public int Id { get; set; }
        public bool HasIssues { get; set; }
        public string HtmlUrl { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
