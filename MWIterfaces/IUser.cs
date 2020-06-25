using MailWarden2.MWUser;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.MWIterfaces
{
    public interface IUser
    {
        string Name { get; set; }
        Team UserTeam { get; set; }
        List<MAPIFolder> Folders { get; set; }
        ThisAddIn App { get; set; }
        MAPIFolder PersonalFolder { get; set; }
    }
}
