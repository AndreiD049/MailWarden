using MailWarden2.MWIterfaces;
using MailWarden2.MWUser;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.Proxies
{
    class UserProxy : IUser
    {
        private IUser _realUser { get; set; }
        public string Name 
        { 
            get
            {
                return RealUser.Name;
            }

            set
            {
                _realUser.Name = value;
            }
        }
        public Team UserTeam
        {
            get
            {
                return RealUser.UserTeam;
            }

            set
            {
                _realUser.UserTeam = value;
            }
        }
        public List<MAPIFolder> Folders
        {

            get
            {
                return RealUser.Folders;
            }

            set
            {
                _realUser.Folders = value;
            }
        }
        public MAPIFolder PersonalFolder
        {
            get
            {
                return RealUser.PersonalFolder;
            }

            set
            {
                _realUser.PersonalFolder = value;
            }
        }
        private IUser RealUser
        {
            get
            {
                if (_realUser == null)
                {
                    _realUser = new User(App);
                }
                return _realUser;
            }
        }
        public ThisAddIn App { get; set; }

        public UserProxy(ThisAddIn app)
        {
            App = app;
        }
    }
}
