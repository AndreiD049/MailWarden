using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MailWarden2.MWIterfaces
{
    public enum MWAction : byte
    {
        Include,
        Exclude
    }

    enum MWFieldNames
    {
        MWSubject,
        MWBody,
        MWFrom,
        MWTo,
        MWCC,
    }
    public interface IRule
    {
        MWAction action { get; set; }
        Regex RgSubject { get; set; }
        Regex RgBody { get; set; }
        Regex RgFrom { get; set; }
        Regex RgTo { get; set; }
        Regex RgCC { get; set; }
        bool IsMailCompliant(MWMailItem mail);
    }

}
