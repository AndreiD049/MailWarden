using MailWarden2.MailMonitor;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.MWIterfaces
{
    public interface IActiveMailMonitor
    {
        Dictionary<string, MonitoredMail> InspectorMails { get; set; }
        Dictionary<string, MonitoredMail> SelectionMails { get; set; }

        void AddInspectorMail(Inspector inspector, MailItem item, MWReplyAction func);
        void ClearMails(Dictionary<string, MonitoredMail> items);
        void NewSelectionMails(List<MailItem> mails, MWReplyAction func);
    }
}
