using MailWarden2.Misc;
using MailWarden2.MWIterfaces;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MailWarden2.MailW
{

    public class MWMailController : IMWMailController
    {
        private ThisAddIn App { get; set; }
        public Dictionary<string, MWMailItem> WatchList { get; set; }

        public MWMailController(ThisAddIn application)
        {
            App = application;
            WatchList = new Dictionary<string, MWMailItem>();
            Startup();
        }

        /// <summary>
        /// Start up by reading all emails from past 5 days + all unread ones.
        /// Start check each mail if it should be handled and HandleExistingMail
        /// </summary>
        public void Startup()
        {
            // Run it in separate thread;
            Task.Run(() =>
            {
                List<MWMailItem> items = GetMWMails();
                foreach (MWMailItem item in items)
                {
                    HandleExistingMail(item);
                }
                // Handle items from DB
                items = App.DBModule.MailTable.SelectAll() as List<MWMailItem>;
                foreach (MWMailItem item in items)
                {
                    HandleExistingMail(item);
                }
            });
        }

        /// <summary>
        /// Adds an item to both current WatchList of the controller and also to WPF to make it visible to the user.
        /// </summary>
        /// <param name="item"></param>
        public void AddToWatchList(MWMailItem item)
        {
            if (!WatchList.ContainsKey(item.EntryID))
            {
                WatchList.Add(item.EntryID, item);
                App?.PaneWPF?.AddItemToView(item);
            }
        }

        public void RemoveFromWatchList(MWMailItem item)
        {
            if (WatchList.ContainsKey(item.EntryID))
            {
                WatchList.Remove(item.EntryID);
                App?.PaneWPF?.RemoveItemFromView(item);
            }
        }

        /// <summary>
        /// Get all unread mails in current users folders
        /// </summary>
        /// <returns>Return the the list of MWMailItem</returns>
        public List<MWMailItem> GetMWMails()
        {
            List<MWMailItem> mails = new List<MWMailItem>();
            foreach (MAPIFolder folder in App.CurrentUser.Folders)
            {
                Items unread = folder.Items.Restrict("[Unread] = true");
                foreach (MailItem mail in unread)
                {
                    mails.Add(new MWMailItem(mail));
                }
            }
            GC.Collect();
            return mails;
        }

        /// <summary>
        /// When a new mail arrives, it will be passed to this funcion.
        /// It should do the following:
        ///  = First check against the rules if the item should be handled
        ///     - Check if item is already in the db
        ///     - if yes
        ///         * check it's status
        ///         * if status = new
        ///             # check if it is visible to the current user
        ///             # if not visible show it (add to the observable collection)
        ///         * if not new, ignore
        ///     - if not in db, add it
        /// </summary>
        /// <param name="item"></param>
        public void HandleExistingMail(MWMailItem item)
        {
            Utils.Debug("HandleExistingMail");
            if (App.Rules.IsMailToBeHandled(item) != MWIterfaces.MWAction.Exclude)
            {
                MWMailItem dbItem = App.DBModule?.MailTable?.SelectOne(item.EntryID) as MWMailItem;
                if (dbItem != null)
                {
                    if (dbItem.status == DBModule.ItemStatus.New)
                    {
                        this.AddToWatchList(item);
                    }
                }
                else
                {
                    // add item to db
                    App.DBModule.MailTable.InsertNew(item);
                    this.AddToWatchList(item);
                }
            }
        }

        /// <summary>
        /// Handle outgoind mail.
        /// When a mail is sent, it shuld check the following on the mail that is being replied:
        ///     = if item should be handled
        ///         - check if item is our watchlist
        ///         - if yes
        ///             * update to processed
        ///             * remove from watchlist and WPF
        ///         - check if item is in db
        ///         - yes:
        ///             * update to processed
        ///         - no:
        ///             * Insert as processed
        /// </summary>
        /// <param name="oldMail">The mail that is being replied</param>
        public void HandleOutgoingMail(MWMailItem oldMail)
        {
            Utils.Debug("HandleOutgoing");
            if (App.Rules.IsMailToBeHandled(oldMail) != MWIterfaces.MWAction.Exclude)
            {
                RemoveFromWatchList(oldMail);
                MWMailItem dbItem = App.DBModule?.MailTable?.SelectOne(oldMail.EntryID) as MWMailItem;
                if (dbItem != null)
                {
                    dbItem.status = DBModule.ItemStatus.Processed;
                    App.DBModule?.MailTable.Update(dbItem);
                }
            }
        }

    }
}
