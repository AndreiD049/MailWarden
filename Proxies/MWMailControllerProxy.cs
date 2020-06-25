using MailWarden2.MailW;
using MailWarden2.MWIterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.Proxies
{
    class MWMailControllerProxy : IMWMailController
    {
        private ThisAddIn App { get; set; }
        private IMWMailController _realController { get; set; }
        private IMWMailController RealController
        {
            get
            {
                if (_realController == null)
                {
                    _realController = new MWMailController(App);
                }
                return _realController;
            }
            
            set
            {
                _realController = value;
            }
        }
        public Dictionary<string, MWMailItem> WatchList
        {
            get
            {
                return RealController.WatchList;
            }

            set
            {
                RealController.WatchList = value;
            }
        }

        public MWMailControllerProxy(ThisAddIn app)
        {
            App = app;
        }

        public void AddToWatchList(MWMailItem item)
        {
            RealController.AddToWatchList(item);
        }

        public List<MWMailItem> GetMWMails()
        {
            return RealController.GetMWMails();
        }

        public void HandleExistingMail(MWMailItem item)
        {
            RealController.HandleExistingMail(item);
        }

        public void HandleOutgoingMail(MWMailItem oldMail)
        {
            RealController.HandleOutgoingMail(oldMail);
        }

        public void RemoveFromWatchList(MWMailItem item)
        {
            RealController.RemoveFromWatchList(item);
        }

        public void Startup()
        {
            RealController.Startup();
        }
    }
}
