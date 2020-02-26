using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
