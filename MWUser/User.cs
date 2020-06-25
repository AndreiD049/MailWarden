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

namespace MailWarden2.MWUser
{
    public class User: IUser
    {
        public string Name { get; set; }
        public Team UserTeam { get; set; }
        public MAPIFolder PersonalFolder { get; set; }
        public List<Items> items { get; set; }
        public List<MAPIFolder> Folders { get; set; }
        private List<string> FolderPaths { get; set; }
        public ThisAddIn App { get; set; }

        public User(ThisAddIn app)
        {
            App = app;
            items = new List<Items>();
            GetUserInfo();
            SetFolderEvents();
        }

        private void GetUserInfo()
        {
            Name = App.Application.Session.CurrentUser.Name;
            UserTeam = GetUserTeam();
            // By Default each user will have his own folder
            PersonalFolder = App.NS.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
            this.Folders = new List<MAPIFolder>() { PersonalFolder };
            // plust folders from the settings
            FolderPaths = GetUserAdditionalFolderPaths();
            SearchAdditionalFolders();
        }

        // Starts the events of AddItem and BeforeMoveItem on users folders
        private void SetFolderEvents()
        {
            foreach (MAPIFolder folder in Folders)
            {
                Debug.WriteLine($"{folder.FolderPath} handler added");
                Items i = folder.Items;
                items.Add(i);
                i.ItemAdd += Items_ItemAdd;
                ((MAPIFolderEvents_12_Event)folder).BeforeItemMove += User_BeforeItemMove;
            }
        }

        private void Items_ItemAdd(object Item)
        {
            MailItem mail = Item as MailItem;
            if (mail != null && mail.ReceivedTime > DateTime.Now.AddHours(-100))
            {
                Debug.WriteLine($"Received");
                if (App.MWContoller != null)
                {
                    MWMailItem MWItem = new MWMailItem(mail);
                    App.MWContoller.HandleExistingMail(MWItem);
                }
            }
        }

        private void User_BeforeItemMove(object Item, MAPIFolder MoveTo, ref bool Cancel)
        {
            MailItem mail = Item as MailItem;
            if (App.MWContoller != null && mail != null)
            {
                MWMailItem MWItem = new MWMailItem(mail);
                App.MWContoller.HandleOutgoingMail(MWItem);
                Debug.WriteLine($"Item moved {MWItem.Subject}");
            }
        }

        /// <summary>
        /// Retrieves the team name from the database
        /// </summary>
        /// <returns></returns>
        private Team GetUserTeam()
        {
            return new Team("Default");
        }

        /// <summary>
        /// Retrieves from the database the list of folders user should watch
        /// </summary>
        /// <returns>The list of string representing folder paths</returns>
        private List<string> GetUserAdditionalFolderPaths()
        {
            // return test data just for test
            return new List<string>() { @"\\andrei.dimitrascu.94@gmail.com\inbox" };
        }

        private void SearchAdditionalFolders()
        {
            foreach (MAPIFolder folder in App.NS.Folders)
            {
                GetFolders(folder);
            }
        }

        private void GetFolders(MAPIFolder folder)
        {
            try
            {
                Utils.Debug($"Try {folder.FullFolderPath}\n");
                /*
                 * We reached an end of a folder chain
                 * There are only mails inside
                 */
                if (folder.Folders.Count == 0)
                {
                    // Check if current folder path is in our FolderPaths list
                    if (FolderPaths.Contains(folder.FullFolderPath.ToLower()))
                    {
                        // if contains, include it into user folders
                        Folders.Add(folder);
                    }
                }
                else // There are still folders inside, check them too
                {
                    foreach (MAPIFolder subfolder in folder.Folders)
                    {
                        GetFolders(subfolder);
                    }
                }
            }
            catch (System.Exception e)
            {
                Utils.Debug($"Error getting folder {e.Message}\n{e.StackTrace}");
            }
        }

    }
}
