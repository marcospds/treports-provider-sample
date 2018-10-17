using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TReportsProviderSample.Dto;

namespace TReportsProviderSample
{
  public class AuthFilter : IActionFilter
  {
    public void OnActionExecuted(ActionExecutedContext context)
    {
      
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    private bool ValidateUser(TReportsRequestBase request)
    {
      return true;
    }
  }
}
