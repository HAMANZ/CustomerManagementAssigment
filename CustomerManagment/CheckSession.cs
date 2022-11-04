using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagment;

namespace CustomerManagment
{
    public class CheckSession : ActionFilterAttribute, IActionFilter
    {
        public string ActionName = "Login";
        public string ControllerName = "Admin";

       
        public override void  OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string Id = context.HttpContext.Session.GetString("Id");
                if (Id == null)
                {
                    context.Result = new RedirectToActionResult("Admin", "Login", null);
                }
            }
            catch (Exception ex)
            {
                context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                                { "Controller", "Error" },
                                { "Action", "Index" }
                         });
            }

        }



    }
}
