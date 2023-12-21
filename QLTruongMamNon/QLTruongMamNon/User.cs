using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTruongMamNon
{
    class User
    {
        static string username;
        public static string Uname
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
    }
}
