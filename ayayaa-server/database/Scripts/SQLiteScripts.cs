using System;
using System.Collections.Generic;
using System.Text;

namespace ayayaa.database.Scripts
{
    public class SQLiteScripts
    {
        public const string CREATE_TABLE_TEST =
            @"CREATE TABLE IF NOT EXISTS [test] (
                [id]           INTEGER      NOT NULL PRIMARY KEY,
                [name]         VARCHAR(256) NOT NULL
            )";

        public const string INSERT_TEST =
            @"INSERT INTO test ([id], [name]) 
            VALUES ";
    }
}
