using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;

namespace MailWarden2.MWIterfaces
{
    public interface IRulesTable
    {
        List<IRule> Rules { get; set; }

        MWAction IsMailToBeHandled(MWMailItem item);
        List<IRule> GetRulesTable();
    }
}
