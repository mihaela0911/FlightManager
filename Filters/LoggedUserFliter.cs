using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Filters
{
    public class LoggedUserFilter : ActionFilterAttribute
    {
        //If the User is not logged
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (AuthUser.LoggedUser is null)
            {
                context.HttpContext.Response.Redirect("/Home/Index");
                context.Result = new EmptyResult();
            }
            base.OnActionExecuting(context);
        }
    }
}
