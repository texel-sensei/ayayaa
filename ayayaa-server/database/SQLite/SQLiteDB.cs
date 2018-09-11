using ayayaa.database.Core;

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
    public class SQLiteDB : IAyayaaDB
    {
        public SQLiteDB(string connectionString)
        {
            connection = new SQLiteConnection(connectionString);
        }

        private SQLiteConnection connection;
        public DbConnection Connection
        {
            get { return connection; }
        }


        public bool CreateDefaultDB()
        {
            if (connection == null)
                return false;

            bool result = false;

            try
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    using (SQLiteTransaction transaction = cmd.Connection.BeginTransaction())
                    {
                        // Create Table Test
                        cmd.CommandText = Scripts.SQLiteScripts.CREATE_TABLE_TEST;
                        cmd.ExecuteNonQuery();

                        // Create table X
                        //cmd.CommandText = Scripts.SQLiteScripts.CREATE_TABLE_X;
                        //cmd.ExecuteNonQuery();

                        // Create table Y
                        //cmd.CommandText = Scripts.SQLiteScripts.CREATE_TABLE_Y;
                        //cmd.ExecuteNonQuery();

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
                connection.Close();
            }

            return result;
        }
    }
}
