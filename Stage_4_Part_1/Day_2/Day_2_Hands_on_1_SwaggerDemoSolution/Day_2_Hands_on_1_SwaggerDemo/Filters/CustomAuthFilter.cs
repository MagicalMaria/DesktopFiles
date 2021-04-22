using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day_2_Hands_on_1_SwaggerDemo.Filters
{
    public class CustomAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var auth = request.Headers["Authorization"];
            if(string.IsNullOrEmpty(auth))
            {
                return;
            }
        }
    }
}
