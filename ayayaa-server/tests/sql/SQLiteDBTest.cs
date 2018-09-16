using ayayaa.database.SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace tests.sql
{
    public class SQLiteDBTest
    {
        private readonly SQLiteDB database = new SQLiteDB("testdb.db");

        [Fact]
        private void CreateTestDB()
        {
            Assert.True(database.CreateDefaultDB());
        }
    }
}
