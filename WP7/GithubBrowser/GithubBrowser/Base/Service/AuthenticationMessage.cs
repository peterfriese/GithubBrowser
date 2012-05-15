using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;

namespace GithubBrowser.Service
{
    class AuthenticationMessage: MessageBase
    {
        public AuthenticationMessage(): base()
        {
        }

        public AuthenticationMessage(bool loggedIn): this()
        {
            LoggedIn = loggedIn;
        }

        private bool _loggedIn = false;
        public bool LoggedIn 
        {
            get
            {
                return _loggedIn;
            }
            set
            {
                _loggedIn = value;
            }
        }

    }
}
