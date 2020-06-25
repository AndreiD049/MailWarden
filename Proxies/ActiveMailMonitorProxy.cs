using MailWarden2.MailMonitor;
using MailWarden2.MWIterfaces;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.Proxies
{
    class ActiveMailMonitorProxy : IActiveMailMonitor
    {
        private IActiveMailMonitor _realMonitor { get; set; }
        private IActiveMailMonitor RealMonitor
        {
            get
            {
                if (_realMonitor == null)
                {
                    _realMonitor = new ActiveMailMonitor();
                }
                return _realMonitor;
            }
        }
        public Dictionary<string, MonitoredMail> InspectorMails
        {
            get
            {
                return RealMonitor.InspectorMails;
            }

            set
            {
                RealMonitor.InspectorMails = value;
            }
        }
        public Dictionary<string, MonitoredMail> SelectionMails
        { 
            get
            {
                return RealMonitor.InspectorMails;
            }

            set
            {
                RealMonitor.InspectorMails = value;
            }

        }

        public void AddInspectorMail(Inspector inspector, MailItem item, MWReplyAction func)
        {
            RealMonitor.AddInspectorMail(inspector, item, func);
        }

        public void ClearMails(Dictionary<string, MonitoredMail> items)
        {
            RealMonitor.ClearMails(items);
        }

        public void NewSelectionMails(List<MailItem> mails, MWReplyAction func)
        {
            RealMonitor.NewSelectionMails(mails, func);
        }
    }
}
