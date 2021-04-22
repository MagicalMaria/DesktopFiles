using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AuthMicroService.Models
{
    public static class UserDetails
    {
        public static string UserName = "user";
        public static string Password = "user";

        public static string GetUserName()
        {
            return UserName;
        }

        public static string GetPassword()
        {
            return Password;
        }
    }
}
