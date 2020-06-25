using MailWarden2.Visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Outlook;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MailWarden2
{
    public partial class AddInRibon
    {
        public ItemEvents_10_ReplyEventHandler hndl { get; set; }
        public MailItem item { get; set; }
        private void AddInRibon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void TestBtn_Click(object sender, RibbonControlEventArgs e)
        {
            Microsoft.Office.Interop.Outlook.Application app = Globals.ThisAddIn.Application;
            item = app.ActiveExplorer().Selection[1] as MailItem;
            hndl = AddInRibon_Reply;
            ((ItemEvents_10_Event)item).Reply += hndl;
        }

        private void AddInRibon_Reply(object Response, ref bool Cancel)
        {
            MessageBox.Show("Replied");
            // ((ItemEvents_10_Event)item).Reply -= hndl;
        }

        private void Unreg_Click(object sender, RibbonControlEventArgs e)
        {
            Microsoft.Office.Interop.Outlook.Application app = Globals.ThisAddIn.Application;
            MailItem item = app.ActiveExplorer().Selection[1] as MailItem;
            Marshal.ReleaseComObject(item);
        }

        private void ShowDebug_Click(object sender, RibbonControlEventArgs e)
        {
            ThisAddIn app = Globals.ThisAddIn;
            if (app.DebugControl == null)
            {
            app.DebugControl = new DebugControl();
            }
            app.DebugControl.Show();
        }
    }
}
