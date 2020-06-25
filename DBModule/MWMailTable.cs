using MailWarden2.Misc;
using MailWarden2.MWIterfaces;
using System;
using System.Data.SQLite;
using MailWarden2.DBModule.Schemas;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace MailWarden2.DBModule
{
    public class MWMailTable : GenericTable, IDBTable
    {
        public static string _TableName { get; set; } = "mails";


        public MWMailTable(MWDBModule module)
        {
            Module = module;
            TableName = _TableName;
            Schema = new MWMailItemSchema();
        }

        public object Delete(object identifier)
        {
            throw new NotImplementedException();
        }

        public object InsertNew(object item)
        {
            try
            {
                MWMailItem mailItem = item as MWMailItem;
                Utils.Debug($"Inserting new record {mailItem.Subject}");
                using (SQLiteConnection connection = new SQLiteConnection(Module.ConnectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = $@"INSERT INTO 
                            {TableName} ({Schema.GetColumnsFormated()})
                            VALUES ({Schema.GetParametersFormated()})";
                        command.Parameters.AddRange(Schema.GetParameters(mailItem).ToArray());
                        using (SQLiteTransaction transaction = connection.BeginTransaction())
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                            return mailItem;
                        }
                    }
                }
            } catch (Exception e)
            {
                Utils.Debug($"Error Inserting new item into {TableName} [InsertNew].\n{e.Message}");
                throw new Exception($"Error Inserting new item into {TableName} [InsertNew].\n{e.Message}");
            }
        }

        public object SelectAll()
        {
            try
            {
                Utils.Debug($"Select all database items");
                using (SQLiteConnection connection = new SQLiteConnection(Module.ConnectionString))
                {
                    connection.Open();
                    using(SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = $@"SELECT * FROM mails Where [status] = @status";
                        command.Parameters.AddWithValue("status", ItemStatus.New.ToString());
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            List<MWMailItem> result = new List<MWMailItem>();
                            while (reader.Read())
                            {
                                result.Add(FromReader(reader));
                            }
                            return result;
                        }
                    }
                }
            } 
            catch (System.Exception e)
            {
                Utils.Debug($"Error getting all items from DB.\n{e.Message}\nStack: {e.StackTrace}");
                throw new Exception($"Error getting all items from DB.\n{e.Message}\nStack: {e.StackTrace}");
            }
        }

        public object SelectOne(object id)
        {
            try
            {
                Utils.Debug($"Select item with id - {id}");
                string entry = id as string;
                using (SQLiteConnection connection = new SQLiteConnection(Module.ConnectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"SELECT * FROM mails WHERE [mail_id] = @mail_id";
                        command.Parameters.AddWithValue("mail_id", entry);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                return null;
                            }
                            reader.Read();
                            return FromReader(reader);
                        }
                    }
                }
            } catch (System.Exception e)
            {
                Utils.Debug($"Error selecting one item from database");
                return null;
            }
        }

        public object Update(object item)
        {
            try
            {
                MWMailItem entry = item as MWMailItem;
                Utils.Debug($"Updating item - {entry.Subject}");
                using (SQLiteConnection connection = new SQLiteConnection(Module.ConnectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = $@"INSERT OR REPLACE into 
                                                {TableName} ({Schema.GetColumnsFormated()})
                                                VALUES ({Schema.GetParametersFormated()})";
                        command.Parameters.AddRange(Schema.GetParameters(entry).ToArray());
                        using (SQLiteTransaction transaction = connection.BeginTransaction())
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                            return entry;
                        }
                    }
                }
            } catch (System.Exception e)
            {
                Utils.Debug($"Error updating entry in DB");
                return null;
            }
        }

        private MWMailItem FromReader(SQLiteDataReader reader)
        {
            try
            {
                return new MWMailItem(
                    reader[Schema.Columns[0]].ToString(),
                    reader[Schema.Columns[1]].ToString(),
                    reader[Schema.Columns[2]].ToString(),
                    DateTime.Parse(reader[Schema.Columns[3]].ToString()),
                    reader[Schema.Columns[4]].ToString(),
                    reader[Schema.Columns[5]].ToString(),
                    reader[Schema.Columns[6]].ToString(),
                    reader[Schema.Columns[7]].ToString(),
                    reader[Schema.Columns[8]].ToString(),
                    reader[Schema.Columns[9]].ToString()
                    ) ;
            }
            catch (FormatException e)
            {
                throw new Exception($"Invalid date time format {reader[Schema.Columns[3]]}");
            }
            catch (Exception e)
            {
                throw new Exception($"Unknow exception.\n{e.Message}\n{e.StackTrace}");
            }
        }

    }
}
