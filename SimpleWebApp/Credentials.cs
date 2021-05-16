using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp
{
    public class Credentials
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public Credentials()
        {

        }

        public Credentials(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }
    }
}
