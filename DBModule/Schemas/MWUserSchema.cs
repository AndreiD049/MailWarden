using MailWarden2.MWIterfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailWarden2.MWUser;

namespace MailWarden2.DBModule.Schemas
{
    class MWUserSchema : GenericSchema
    {
        public MWUserSchema()
        {
            Columns = new List<string>
            {
                "user_name",
                "team_name"
            };

            CreationColumns = new List<string>
            {
                "[user_name] TEXT PRIMARY KEY ON CONFLICT IGNORE NOT NULL",
                "[team_name] TEXT",
                "FOREIGN KEY(team_name) REFERENCES teams(team_name)"
            };
        }

        public override List<SQLiteParameter> GetParameters(object el)
        {
            User user = el as User;
            List<SQLiteParameter> result = new List<SQLiteParameter>(); 
            for (int i = 0; i < Columns.Count; i++)
            {
                switch (Columns[i])
                {
                    case "user_name":
                        result.Add(new SQLiteParameter("user_name", user.Name));
                        break;
                    case "team_name":
                        result.Add(new SQLiteParameter("team_name", user.UserTeam.Name));
                        break;
                }
            }
            return result;
        }

    }
}
