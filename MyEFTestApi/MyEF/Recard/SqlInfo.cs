using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEF.Recard
{
    public  record struct SqlInfo(string ColumnSql,string TableName,string Id)
    {
    }
}
