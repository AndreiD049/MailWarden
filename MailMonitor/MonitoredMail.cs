using MailWarden2.Misc;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace MailWarden2.MailMonitor
{
    public delegate void MWReplyAction(MailItem item);
    public class MonitoredMail
    {
        public MailItem item { get; set; }
        public ItemEvents_10_ReplyEventHandler ReplyHandler { get; set; }
        public ItemEvents_10_ReplyAllEventHandler ReplyAllHandler { get; set; }
        public ItemEvents_10_ForwardEventHandler ForwardHandler { get; set; }
        public MWReplyAction action { get; set; }

        public MonitoredMail(MailItem item, MWReplyAction func)
        {
                this.item = item;
                action = func;
                ReplyHandler = SetSendHandler;
                ReplyAllHandler = SetSendHandler;
                ForwardHandler = SetSendHandler;
                ((ItemEvents_10_Event)this.item).Reply += ReplyHandler;
                ((ItemEvents_10_Event)this.item).ReplyAll += ReplyAllHandler;
                ((ItemEvents_10_Event)this.item).Forward += ForwardHandler;
                Utils.Debug("Handlers are set");
        }

        private void SetSendHandler(object item, ref bool cancel)
        {
            MailItem mail = item as MailItem;
            ((ItemEvents_10_Event)mail).Send += (ref bool c) => { action(this.item); };
        }

        public void UnsubscribeMail()
        {
            ((ItemEvents_10_Event)item).Reply -= ReplyHandler;
            ((ItemEvents_10_Event)item).ReplyAll -= ReplyAllHandler;
            ((ItemEvents_10_Event)item).Forward-= ForwardHandler;
        }
    }
}
