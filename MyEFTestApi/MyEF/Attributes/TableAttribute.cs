using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEF.Attributes
{
    public class TableAttribute : Attribute
    {
        private readonly string name;

        public TableAttribute(string name)
        {
            this.name = name;
        }
        public string GetName()
        {
            return name;
        }
    }
}
