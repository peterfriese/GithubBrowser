using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GithubBrowser.Model
{
    public class Commit
    {
        public CommitUser Author { get; set; }
        public CommitUser Committer { get; set; }
        public String Message { get; set; }
        public String Url { get; set; }
    }
}
