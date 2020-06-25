using MailWarden2.DBModule;
using MailWarden2.Misc;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2
{

    public class MWMailItem: INotifyPropertyChanged
    {
        public string EntryID { get; set; }
        public string FolderEntryID { get; set; }
        public string Subject { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public ItemStatus status { get; set; }
        public string mail_user { get; set; }
        public string Body { get; set; }
        public string Timer { get; set; }
        /*
         * Not a part of database schema
         */
        public string FolderPath
        {
            get
            {
                try
                {
                    MAPIFolder folder = Globals.ThisAddIn?.NS?.GetFolderFromID(FolderEntryID);
                    return folder == null ? "Unknown" : folder.FolderPath;
                }
                catch (COMException e)
                {
                    return "Unknown";
                }
            }
        }

        public MWMailItem(MailItem item)
        {
            MAPIFolder folder = item.Parent as MAPIFolder;
            EntryID = item.EntryID;
            FolderEntryID = folder.EntryID;
            Subject = item.Subject;
            ReceivedDate = item.ReceivedTime;
            From = item.SenderEmailAddress;
            // Get all recipients ; - delimited
            string recipients = "";
            foreach (Recipient recipient in item.Recipients)
            {
                recipients += recipient.Address + ";";
            }
            To = recipients;
            CC = item.CC;
            status = ItemStatus.New;
            // chek if this mail is not the users personal folder
            if (((MAPIFolder)item.Parent).EntryID == Globals.ThisAddIn.CurrentUser.PersonalFolder.EntryID)
            {
                mail_user = Globals.ThisAddIn.CurrentUser.Name;
            }
            Body = item.Body;
            Timer = Utils.GetDifference(ReceivedDate); 
        }

        public MWMailItem(string entryid, string fodlerid, string subject, DateTime receivedDate, string from, string to="", string cc="", string stat="New", string muser="", string body="")
        {
            EntryID = entryid;
            FolderEntryID = fodlerid;
            Subject = subject;
            ReceivedDate = receivedDate;
            From = from;
            To = to;
            CC = cc;
            mail_user = muser;
            Body = body;
            if (Enum.TryParse(stat, out ItemStatus parsedStatus))
            {
                status = parsedStatus;
            }
            else
            {
                throw new System.Exception($"Invalid status {stat}");
            }

            Timer = Utils.GetDifference(ReceivedDate); 
        }

        public void UpdateTimer()
        {
            Timer = Utils.GetDifference(ReceivedDate);
            OnPropertyChanged("Timer");
        }

        public override string ToString()
        {
            return $"=======MAILITEM=======\n" +
                   $"Entry ID: {EntryID}\n" +
                   $"Folder Entry ID: {FolderEntryID}\n" +
                   $"Folder Name: {FolderPath}\n" +
                   $"Subject: {Subject}\n" +
                   $"Received Date: {ReceivedDate.ToString("dd/MM/yyyy hh:mm:ss")}\n" +
                   $"From: {From}\n" +
                   $"To: {To}\n" +
                   $"CC: {CC}\n" +
                   $"======================\n";
        }

        // INotifyProperty Changed Stuff
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


    }
}
