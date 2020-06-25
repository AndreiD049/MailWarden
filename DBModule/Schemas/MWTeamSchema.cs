using MailWarden2.MWUser;
using MailWarden2.MWIterfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.DBModule.Schemas
{
    class MWTeamSchema: GenericSchema
    {
        public MWTeamSchema()
        {
            Columns = new List<string>
            {
                "team_name"
            };

            CreationColumns = new List<string>
            {
                "[team_name] TEXT PRIMARY KEY ON CONFLICT IGNORE NOT NULL",
            };
        }

        public override List<SQLiteParameter> GetParameters(object el)
        {
            Team team = el as Team;
            List<SQLiteParameter> result = new List<SQLiteParameter>(); 
            for (int i = 0; i < Columns.Count; i++)
            {
                switch (Columns[i])
                {
                    case "team_name":
                        result.Add(new SQLiteParameter("team_name", team.Name));
                        break;
                }
            }
            return result;
        }
    }
}
