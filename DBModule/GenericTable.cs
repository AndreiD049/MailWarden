using MailWarden2.MWIterfaces;
using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailWarden2.DBModule
{
    public abstract class GenericTable 
    {
        public IDBModule Module { get; set; }
        public ISchema Schema { get; set; }
        public string TableName { get; set; }

        public void InitTable()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(this.Module.ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand command = new SQLiteCommand(conn))
                    {
                        command.CommandText = $@"CREATE TABLE IF NOT EXISTS [{TableName}] ({String.Join(",", Schema.CreationColumns)});";
                        // create table in transaction
                        using (SQLiteTransaction transaction = conn.BeginTransaction())
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }
            } catch (Exception e)
            {
                throw new Exception($"Error creating database table [InitTable] - {TableName}.\n{e.Message}");
            }
        }

    }
}
