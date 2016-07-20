using _Proje_Blog_.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _Proje_Blog_.Filters
{
    public class LoginGerektirirAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!UserHelper.Id.HasValue)
            {
                filterContext.HttpContext.Response.Redirect("~/Login/Index");
            }
        }
    }
}