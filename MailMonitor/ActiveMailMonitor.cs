using MailWarden2.Misc;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using MailWarden2.MWIterfaces;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.MailMonitor
{
    public class ActiveMailMonitor : IActiveMailMonitor
    {
        public Dictionary<string, MonitoredMail> SelectionMails { get; set; }
        public Dictionary<string, MonitoredMail> InspectorMails { get; set; }
        public ActiveMailMonitor()
        {
            SelectionMails = new Dictionary<string, MonitoredMail>();
            InspectorMails = new Dictionary<string, MonitoredMail>();
        }

        public void ClearMails(Dictionary<string, MonitoredMail> items)
        {
            int count = items.Count;
            foreach (string key in items.Keys)
            {
                items[key].UnsubscribeMail();
            }
            items.Clear();
            // Free resources if too many used
            if (count > 1)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        /// <summary>
        /// This action is performed when selection of mails changes in outlook
        /// </summary>
        /// <param name="mails">List of selected mailitems</param>
        /// <param name="func">
        /// function, that gets 1 argument, the original MailItem. This function should implement
        /// the actions we want to do when replying/forwarding the mail
        /// </param>
        public void NewSelectionMails(List<MailItem> mails, MWReplyAction func)
        {
            // check if mails are the same as SelectionMails
            if (mails.Count == SelectionMails.Count)
            {
                bool same = true;
                foreach (MailItem mail in mails)
                {
                    if (!SelectionMails.ContainsKey(mail.EntryID))
                        same = false;
                }
                // Lists are the same, no need to create new objects
                if (same)
                    return;
            }
            // first clear the current selection, and unsubscribe
            this.ClearMails(SelectionMails);
            // add each mail in selection to the monitor
            foreach (MailItem mail in mails)
            {
                if (Utils.CheckItemValid(mail))
                {
                    Utils.Debug($"Add selection mail {mail.Subject}");
                    SelectionMails.Add(mail.EntryID, new MonitoredMail(mail, func));
                }
            }
        }

        public void AddInspectorMail(Inspector inspector, MailItem item, MWReplyAction func)
        {
            /* we need to add to inspector only if it's an existing mail, not a draft one
            * Even if we already have this mail in selection items, we might select other mails while 
            * the inspector is still open in the background
            */
            if (item.EntryID != null)
            {
                Utils.Debug($"New Inspector mail added {item.Subject}");
                MonitoredMail m = new MonitoredMail(item, func);
                InspectorMails.Add(item.EntryID, m);
                ((InspectorEvents_10_Event)inspector).Close += () => { RemoveInspectorMail(item); };
            }
        }

        private void RemoveInspectorMail(MailItem item)
        {
            Utils.Debug($"Cleared Inspector mail of {item.Subject}");
            if (InspectorMails.ContainsKey(item.EntryID))
            {
                InspectorMails[item.EntryID].UnsubscribeMail();
                InspectorMails.Remove(item.EntryID);
            }
            GC.Collect();
        }
    }
}
