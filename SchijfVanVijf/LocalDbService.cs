using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchijfVanVijf
{
    public class LocalDbService
    {
        private const string DbFileName = "schijfvanvijf.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(DbFileName);
        }
    }
}
