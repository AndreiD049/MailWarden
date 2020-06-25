using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.Visuals
{
    public class GroupedMails
    {
        public string FolderName { get; set; }
        public ObservableCollection<MWMailItem> Mails { get; set; }
        public GroupedMails(string name, List<MWMailItem> items)
        {
            FolderName = name;
            Mails = new ObservableCollection<MWMailItem>(items);
        }
    }
}
