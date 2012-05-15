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
    public class CommitWrapper: CommitNode
    {
        public User Author { get; set; }
        public User Committer { get; set; }
        public Commit Commit { get; set; }
    }
}
