using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationAPI.Models
{
    public class RegistrationModel
    {
        public string RegID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] ProfilePhoto { get; set; }

    }
}