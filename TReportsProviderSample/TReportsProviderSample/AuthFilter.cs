using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TReportsProviderSample.Classes;

namespace TReportsProviderSample
{
  public class AuthFilter : IActionFilter
  {
    public void OnActionExecuted(ActionExecutedContext context)
    {
      
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      TReportsRequestBase request = null;

      if (context.ActionArguments.ContainsKey("request"))
        request = context.ActionArguments["request"] as TReportsRequestBase;
      else
      {
        Stream stream = context.HttpContext.Request.Body;
        using (StreamReader reader = new StreamReader(stream))
          request = JsonConvert.DeserializeObject<TReportsRequestBase>(reader.ReadToEnd());
      }
          
      if (request != null && !ValidateUser(request))
      {
        context.Result = new ObjectResult("Not Authorized");
        context.HttpContext.Response.StatusCode = 401;
      }
    }

    private bool ValidateUser(TReportsRequestBase request)
    {
      string user = request.ProviderParams.Where(p => p.Name == "user").ElementAt(0).Value;
      string pass = request.ProviderParams.Where(p => p.Name == "password").ElementAt(0).Value;

      if (user != "treports" || pass != "treports")
      {
        return false;
      }

      return true;
    }
  }
}
