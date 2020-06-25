using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.Misc
{
    static class Utils
    {
        public static void Debug(String message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            Globals.ThisAddIn?.DebugControl?.Invoke(Globals.ThisAddIn.DebugControl.DebugMessage, message);
        }
        
        public static bool WasMailReplied(MailItem item)
        {
            return item.PropertyAccessor.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x10810003").ToString() == "102";
        }

        public static bool WasMailForwarded(MailItem item)
        {
            return item.PropertyAccessor.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x10810003").ToString() == "104";
        }

        public static string GetDifference(DateTime dt)
        {
            TimeSpan ts = DateTime.Now - dt;
            return $"{String.Format("{0:00}", Convert.ToInt32(ts.TotalHours))}:{String.Format("{0:00}", Convert.ToInt32(ts.Minutes))}";
        }

        public static bool CheckItemValid(MailItem item)
        {
            try
            {
                return item.Parent != null;
            }
            catch (COMException)
            {
                return false;
            }
        }
    }
}
