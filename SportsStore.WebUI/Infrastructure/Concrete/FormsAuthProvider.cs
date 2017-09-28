using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.WebUI.Infrastructure.Abstract;
using System.Web.Security;

namespace SportsStore.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string userName, string passWord)
        {
            bool result = FormsAuthentication.Authenticate(userName, passWord);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(userName, false);
            }
            return result;
        }
    }
}