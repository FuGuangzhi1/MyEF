using MyEFCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEFCore
{
    public class DbContextOptionsBuilder
    {
        public MyDBType dBType;
        public string? dbConnectionString;
        public DbContextOptionsBuilder UseSqlSever(string conn)
        {
            dBType = MyDBType.SqlSever;
            dbConnectionString = conn;
            return this;
        }
    }
}
