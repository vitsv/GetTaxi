using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain
{
    public partial class User
    {
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        public bool Active
        {
            get
            {
                return (!Blocked && !Locked);
            }
        }


        public bool Blocked
        {
            get
            {
                return this.BlockDate.HasValue && (this.BlockDate.Value.CompareTo(System.DateTime.Today) <= 0);
            }
        }

        public bool Locked
        {
            get
            {
                return this.SuspendDate.HasValue && (this.SuspendDate.Value.CompareTo(DateTime.Now) > 0);
            }
        }
    }
}
