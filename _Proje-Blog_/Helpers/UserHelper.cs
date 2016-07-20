using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Proje_Blog_.Helpers
{
    public class UserHelper
    {
        public static string IPAdress
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }
        public static int? Id
        {
            get { return (int?)HttpContext.Current.Session["id"]; }
            set { HttpContext.Current.Session["id"] = value; }
        }
        public static string UserName
        {
            get { return HttpContext.Current.Session["Username"].ToString(); }
            set { HttpContext.Current.Session["Username"] = value; }
        }
    }
}