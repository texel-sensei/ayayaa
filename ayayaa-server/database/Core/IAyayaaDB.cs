using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ayayaa.database.Core
{
    public interface IAyayaaDB
    {
        DbConnection Connection { get; }

        bool CreateDefaultDB();
    }
}
