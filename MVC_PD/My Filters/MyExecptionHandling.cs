using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC_PD.My_Filters
{
    public class MyExecptionHandling:ActionFilterAttribute // put it as attribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception !=null)
            {
                context.ExceptionHandled = true;
                context.Result = new ContentResult()
                {
                    Content = "Contact Admin"
                };
            
            }

            base.OnActionExecuted(context);                         



        }


    }
}
