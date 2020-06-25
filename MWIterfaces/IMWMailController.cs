using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.MWIterfaces
{
    public interface IMWMailController
    {
        Dictionary<string, MWMailItem> WatchList { get; set; }

        void AddToWatchList(MWMailItem item);
        List<MWMailItem> GetMWMails();
        void HandleExistingMail(MWMailItem item);
        void HandleOutgoingMail(MWMailItem oldMail);
        void RemoveFromWatchList(MWMailItem item);
        void Startup();
    }
}
