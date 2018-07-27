using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Classes
{
  public class TReportsRequestBase
  {
    [JsonProperty("providerParams")]
    public TReportsProviderParams[] ProviderParams { get; set; }
  }  
}
