using MailWarden2.Misc;
using System.Data.SQLite;
using MailWarden2.MWIterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using MailWarden2.MWUser;
using System.Data.Entity.Core.Metadata.Edm;

namespace MailWarden2.DBModule
{
    public enum ItemStatus
    {
        New,
        Pending,
        Processed
    }
    public enum TablesEnum
    {
        MailItems,
        Users,
        Rules,
        Teams,
    }
    public class MWDBModule: IDBModule
    {
        public string Path { get; set; }
        public string ConnectionString { get; set; }
        public Dictionary<TablesEnum, IDBTable> Tables { get; set; }
        public MWMailTable MailTable { get; set; }

        public MWDBModule(string Path)
        {
            this.Path = Path;
            this.ConnectionString = $"Data Source={Path}";
            // Add all tables to the database
            Tables = new Dictionary<TablesEnum, IDBTable>();
            MailTable = new MWMailTable(this);
            Tables.Add(TablesEnum.MailItems, MailTable);
            Tables.Add(TablesEnum.Users, new MWUserTable(this));
            Tables.Add(TablesEnum.Teams, new MWTeamTable(this));
            // Initialize the database
            InitDb();
            TestDb();
        }

        public void TestDb()
        {
            //this.Tables[TablesEnum.MailItems].InsertNew(new MWMailItem("123", "1234", "Subject test", DateTime.Now, "andrei@mail.test"));
            //this.Tables[TablesEnum.Users].InsertNew(new User(Globals.ThisAddIn));
            //this.Tables[TablesEnum.Teams].InsertNew(new Team("Test team"));
            //this.Tables[TablesEnum.MailItems].SelectOne("123");
            //this.Tables[TablesEnum.MailItems].Update(new MWMailItem("123", "1234", "Subject test", DateTime.Now, "andrei@mail.test", stat:"Processed"));
        }

        public bool InitDb()
        {
            try
            {
                Utils.Debug("Initializing DB");
                if (!File.Exists(this.Path))
                    SQLiteConnection.CreateFile(this.Path);

                foreach (IDBTable table in Tables.Values)
                {
                    table.InitTable();
                }
                return true;
            }
            catch (System.Exception e)
            {
                Utils.Debug($"Exception initializing the database {e.Message}");
                return false;
            }
        }

    }
}
