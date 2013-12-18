using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class UserData
    {
        public string FullName { get; set; }

        public int[] Roles { get; set; }

        public int ID { get; set; }

        public UserData()
        {
            FullName = "Unknow";

            ID = 0;
        }
    }
}