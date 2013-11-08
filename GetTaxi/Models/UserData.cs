using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translator.Models
{
    public class UserData
    {
        public string FullName { get; set; }

        public string Role { get; set; }

        public int RightsSum { get; set; }

        public int ID { get; set; }

        public UserData()
        {
            FullName = "Unknow";

            RightsSum = 0;

            ID = 0;
        }
    }
}