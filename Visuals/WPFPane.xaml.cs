using MailWarden2.Visuals;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MailWarden2
{
    /// <summary>
    /// Interaction logic for WPFPane.xaml
    /// </summary>
    public partial class WPFPane : UserControl
    {
        public ObservableCollection<GroupedMails> Data { get; set; }
        public Dictionary<string, ObservableCollection<MWMailItem>> Map { get; set; }
        public WPFPane()
        {
            InitializeComponent();
            Data = new ObservableCollection<GroupedMails>();
            Map = new Dictionary<string, ObservableCollection<MWMailItem>>();
            Mails.ItemsSource = Data;
            StartTimer();
        }

        public void StartTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimers);
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Start();
        }
        
        public void InitData()
        {
            foreach (MAPIFolder folder in Globals.ThisAddIn.CurrentUser.Folders)
            {
                GroupedMails group = new GroupedMails(folder.FolderPath, new List<MWMailItem>());
                Data.Add(group);
                Map.Add(folder.FolderPath, group.Mails);
            }
        }

        public void AddItemToView(MWMailItem item)
        {
            this.Dispatcher.Invoke(() =>
            {
                string FolderPath = item.FolderPath;
                if (Map.ContainsKey(FolderPath))
                {
                    Map[FolderPath].Add(item);
                }
                else
                {
                    GroupedMails group = new GroupedMails(FolderPath, new List<MWMailItem>());
                    Data.Add(group);
                    Map.Add(FolderPath, group.Mails);
                    group.Mails.Add(item);
                }
            });
        }

        public void RemoveItemFromView(MWMailItem item)
        {
            this.Dispatcher.Invoke(() =>
            {
                string FolderPath = item.FolderPath;
                if (Map.ContainsKey(FolderPath))
                {
                    MWMailItem found = (from i in Map[FolderPath] where i.EntryID == item.EntryID select i).First();
                    Map[FolderPath].Remove(found);
                }
            });
        }

        private void UpdateTimers(object sender, EventArgs e)
        {
            MailWarden2.Misc.Utils.Debug("Update timer");
            foreach (GroupedMails group in Data)
            {
                foreach (MWMailItem item in group.Mails)
                {
                    item.UpdateTimer();
                }
            }
            Mails.UpdateLayout();
            Mails.ItemsSource = Data;
        }

        private void MailItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MWMailItem item = ((TreeViewItem)sender).DataContext as MWMailItem;
            if (item != null)
            {
                try
                {
                    MailItem oItem = Globals.ThisAddIn.NS.GetItemFromID(item.EntryID) as MailItem;
                    // Run in thread, because new window is opened in the background
                    Task.Run(() => { oItem.Display(); });
                } 
                catch (COMException ex)
                {
                    MessageBox.Show($"Message cannot be found.\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
