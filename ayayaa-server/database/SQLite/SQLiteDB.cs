using ayayaa.logging;
using ayayaa.logging.Enums;
using ayayaa.logging.Writers;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace ayayaa.database.SQLite
{
    public class SQLiteDB
    {
        public SQLiteDB(string filePath)
        {
            System.Data.SQLite.SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = filePath;
            Connection = new SQLiteConnection(builder.ConnectionString);
        }


        public SQLiteConnection Connection { get; }

        public bool CreateDefaultDB()
        {
            if (Connection == null)
                return false;

            bool result = false;

            try
            {
                Connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(Connection))
                {
                    using (SQLiteTransaction transaction = cmd.Connection.BeginTransaction())
                    {
                        // Create Table Test
                        cmd.CommandText = Scripts.SQLiteScripts.CREATE_TABLE_TEST;
                        cmd.ExecuteNonQuery();

                        // Insert data for table test
                        for (int i = 0; i < 10; i++)
                        {
                            string insert = Scripts.SQLiteScripts.INSERT_TEST;
                            string values = string.Format("({0}, '{1}');", i, "test" + i);
                            cmd.CommandText = insert + values;
                            cmd.ExecuteNonQuery();
                        }


                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                // LOG
            }
            finally
            {
                Connection.Close();
            }

            return result;
        }
    }
}
