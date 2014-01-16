using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain
{
    public partial class Client
    {
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }
    }
}
