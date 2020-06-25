using MailWarden2.MWUser;
using MailWarden2.MWIterfaces;
using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailWarden2.DBModule.Schemas;

namespace MailWarden2.DBModule
{
    class MWUserTable : GenericTable, IDBTable
    {
        public static string _TableName { get; set; } = "users";

        public MWUserTable(MWDBModule module)
        {
            Module = module;
            TableName = _TableName;
            Schema = new MWUserSchema();
        }

        public object Delete(object identifier)
        {
            throw new NotImplementedException();
        }

        public object InsertNew(object item)
        {
            try
            {
                User user = item as User;
                using (SQLiteConnection connection = new SQLiteConnection(Module.ConnectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = $@"INSERT INTO 
                            {TableName} ({Schema.GetColumnsFormated()})
                            VALUES ({Schema.GetParametersFormated()})";
                        command.Parameters.AddRange(Schema.GetParameters(user).ToArray());
                        using (SQLiteTransaction transaction = connection.BeginTransaction())
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                            return user;
                        }
                    }
                }
            } catch (Exception e)
            {
                throw new Exception($"Error Inserting new item into {TableName} [InsertNew].\n{e.Message}");
            }
        }

        public object SelectAll()
        {
            throw new NotImplementedException();
        }

        public object SelectOne(object id)
        {
            throw new NotImplementedException();
        }

        public object Update(object item)
        {
            throw new NotImplementedException();
        }
    }
}
