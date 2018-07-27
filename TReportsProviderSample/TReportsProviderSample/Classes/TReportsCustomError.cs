using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TReportsProviderSample.Classes
{
  public class TReportsCustomError : TReportsResponseBase
  {
    public string code { get; set; }
    public string message { get; set; }
    public string detailedMessage { get; set; }
  }
}

