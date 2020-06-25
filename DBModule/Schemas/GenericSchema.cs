using MailWarden2.MWIterfaces;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.DBModule.Schemas
{
    abstract class GenericSchema: ISchema
    {
        public const string createdOnColumn = "created_on";
        public const string modifiedOnColumn = "modified_on";
        public const string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public List<string> Columns { get; set; }
        public List<string> CreationColumns { get; set; }
        public GenericSchema()
        {

        }
        public abstract List<SQLiteParameter> GetParameters(object el);

        public string GetColumnsFormated()
        {
            return String.Join(",", Columns);
        }

        public string GetParametersFormated()
        {
            return String.Join(",", Columns.Select(p => $"@{p}"));
        }
    }
}
