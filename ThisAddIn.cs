using MailWarden2.MWUser;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Windows.Forms;
using System.Collections;
using System.Linq;
using MailWarden2.Misc;
using MailWarden2.MWIterfaces;
using System.Collections.Generic;
using MailWarden2.MailMonitor;
using MailWarden2.Rules;
using System.Diagnostics;
using MailWarden2.MailW;
using MailWarden2.DBModule;
using System.Threading;
using MailWarden2.Proxies;
using MailWarden2.Visuals;
using System.IO;

namespace MailWarden2
{
    public partial class ThisAddIn
    {
        public NameSpace NS { get; set; }
        public WPFPane PaneWPF { get; set; }
        public IActiveMailMonitor Monitor { get; set; }
        public IRulesTable Rules { get; set; }
        public IUser CurrentUser { get; set; }
        public IMWMailController MWContoller { get; set; }
        public Explorer explorer { get; set; }
        public IDBModule DBModule { get; set; }
        public DebugControl DebugControl { get; set; }
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            try
            {
                NS = Application.GetNamespace("MAPI");
                InitTaskPane();
                RegisterEvents();
            }
            catch (System.Exception err)
            {
                MessageBox.Show($"Error occured {err.Message}\n{err.StackTrace}");
            }
        }

        private void Application_Startup()
        {
            // TODO: refactor below
            new Thread(() =>
            {
                Rules = new RulesTableProxy();
                CurrentUser = new UserProxy(this);
                Monitor = new ActiveMailMonitorProxy();
                DBModule = new MWDBModuleProxy($@"{Path.GetTempPath()}/test.db");
                MWContoller = new MWMailControllerProxy(this);
                UpdateWPF();
                MWContoller.Startup();
            }).Start();
        }

        private void RegisterEvents()
        {
            explorer = this.Application.ActiveExplorer();
            explorer.SelectionChange += Explorer_SelectionChange;
            Application.Inspectors.NewInspector += Inspectors_NewInspector;
            //Application.NewMailEx += Application_NewMailEx;
            Application.Startup += Application_Startup;
        }

        public void OpenTaskPane(UserControl control, string title=null)
        {
            Microsoft.Office.Tools.CustomTaskPane TaskPane = this.CustomTaskPanes.Add(control, title);
            TaskPane.Visible = true;
        }

        private void Application_NewMailEx(string EntryIDCollection)
        {
            MWMailItem item = new MWMailItem(NS.GetItemFromID(EntryIDCollection) as MailItem);
            // if item received was a mail (it could be a meeting invitation of something else)
            if (item != null)
            {
                Utils.Debug($"New mail received.\n{item}");
                this.MWContoller.HandleExistingMail(item);
            }
        }

        private void Inspectors_NewInspector(Inspector Inspector)
        {
            MailItem item = Inspector.CurrentItem as MailItem;
            if (item != null)
            {
                Monitor.AddInspectorMail(Inspector, item, (MailItem i) => { Utils.Debug(item.Subject); this.MWContoller.HandleOutgoingMail(new MWMailItem(item)); });
            }
       }

        private void Explorer_SelectionChange()
        {
            Utils.Debug("Selection Changed");
            Explorer explorer = Application.ActiveExplorer();
            Selection selection = explorer.Selection;
            if (selection != null && selection.Count > 0)
            {
                List<MailItem> selectedMails = new List<MailItem>(explorer.Selection.OfType<MailItem>().Take(10));
                if (selectedMails.Count > 0)
                {
                    Monitor.NewSelectionMails(selectedMails, (MailItem item) => { Utils.Debug(item.Subject); this.MWContoller.HandleOutgoingMail(new MWMailItem(item)); });
                }
            }
        }

        /// <summary>
        /// Updating the WPF panel after User was loaded
        /// </summary>
        public void UpdateWPF()
        {
            this.PaneWPF?.Dispatcher.Invoke(() => { this.PaneWPF?.InitData(); });
        }

        private void InitTaskPane()
        {
            Pane _p = new Pane();
            PaneWPF = _p.PaneWPF;
            OpenTaskPane(_p, "MW");
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
