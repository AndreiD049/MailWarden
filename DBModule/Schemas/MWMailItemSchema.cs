using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.DBModule.Schemas
{
    class MWMailItemSchema: GenericSchema
    {
        public MWMailItemSchema(): base()
        {

            Columns = new List<string>
            {
                "mail_id",
                "folder_id",
                "subject",
                "received_date",
                "mail_from",
                "mail_to",
                "mail_cc",
                "status",
                "mail_user",
                "body"
            };

            CreationColumns = new List<string>()
            {
                "[mail_id] TEXT PRIMARY KEY ON CONFLICT IGNORE NOT NULL",
                "[folder_id] TEXT",
                "[subject] TEXT",
                "[mail_from] TEXT NOT NULL",
                "[mail_to] TEXT",
                "[mail_cc] TEXT",
                "[received_date] DATETIME NOT NULL",
                $"[status] TEXT DEFAULT '{ItemStatus.New}'",
                "[mail_user] TEXT",
                "[body] TEXT",
                $"[{createdOnColumn}] DATETIME DEFAULT CURRENT_TIMESTAMP",
                $"[{modifiedOnColumn}] DATETIME DEFAULT CURRENT_TIMESTAMP",
                "FOREIGN KEY(mail_user) REFERENCES users(user_name)",
            };

        }

        public override List<SQLiteParameter> GetParameters(object el)
        {
            MWMailItem item = el as MWMailItem;
            List<SQLiteParameter> result = new List<SQLiteParameter>(); 
            for (int i = 0; i < Columns.Count; i++)
            {
                switch (Columns[i])
                {
                    case "mail_id":
                        result.Add(new SQLiteParameter("mail_id", item.EntryID));
                        break;
                    case "folder_id":
                        result.Add(new SQLiteParameter("folder_id", item.FolderEntryID));
                        break;
                    case "subject":
                        result.Add(new SQLiteParameter("subject", item.Subject));
                        break;
                    case "mail_from":
                        result.Add(new SQLiteParameter("mail_from", item.From));
                        break;
                    case "mail_to":
                        result.Add(new SQLiteParameter("mail_to", item.To));
                        break;
                    case "mail_cc":
                        result.Add(new SQLiteParameter("mail_cc", item.CC));
                        break;
                    case "received_date":
                        result.Add(new SQLiteParameter("received_date", item.ReceivedDate.ToString(dateTimeFormat)));
                        break;
                    case "status":
                        result.Add(new SQLiteParameter("status", item.status.ToString()));
                        break;
                    case "mail_user":
                        result.Add(new SQLiteParameter("mail_user", item.mail_user));
                        break;
                    case "body":
                        result.Add(new SQLiteParameter("body", item.Body));
                        break;
                }
            }
            return result;
        }

    }
}
