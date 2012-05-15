using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;

namespace GithubBrowser.ViewModel
{
    public class ProgressMessage: MessageBase
    {
        private int _loadingParties;

        public ProgressMessage(int loadingParties)
        {
            this._loadingParties = loadingParties;
        }

        public bool IsActive
        {
            get
            {
                return _loadingParties > 0;
            }
        }
    }
}
