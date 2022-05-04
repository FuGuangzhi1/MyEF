using MyEF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEF.Config
{
    public class DbContextOptions
    {
        public MyDBType myDBType;
        public string? conn;
        public DbContextOptions UseSqlSever(string conn)
        {
            this.myDBType = MyDBType.SqlSever;
            this.conn = conn;
            return this;
        }
    }
}
