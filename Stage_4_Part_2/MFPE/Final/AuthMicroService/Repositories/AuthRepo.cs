using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthMicroService.Models;
using log4net;

namespace AuthMicroService.Repositories
{
    public class AuthRepo : IAuthRepo
    {
        //private Credentials UserCredentials = new Credentials()
        //{
        //    UserName = UserDetails.GetUserName(),
        //    Password = UserDetails.GetPassword()
        //};
        private readonly ILog _log4net = LogManager.GetLogger(typeof(AuthRepo));

        public Credentials GetCredentials()
        {
            try
            {
                //var result = UserCredentials;
                var userCredentials = new Credentials()
                {
                    UserName = UserDetails.GetUserName(),       //staticClassName.MethodName()
                    Password = UserDetails.GetPassword()
                };
                return userCredentials;
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(GetCredentials));
                return null;
            }
        }
    }
}
