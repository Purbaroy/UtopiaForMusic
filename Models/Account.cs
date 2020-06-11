using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMT563_Utopia.Models
{
    public class Account
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
    public class AccountCreate
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }

    }
}