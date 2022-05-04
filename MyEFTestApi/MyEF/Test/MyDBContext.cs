using MyEF.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEF.Test
{
    public class MyDBContext : DBContext
    {
        public MyDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
