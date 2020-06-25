using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.MWIterfaces
{
    public interface ISchema
    {
        List<string> Columns { get; set; }
        List<string> CreationColumns { get; set; }
        List<SQLiteParameter> GetParameters(object el);
        string GetColumnsFormated();
        string GetParametersFormated();
    }
}
