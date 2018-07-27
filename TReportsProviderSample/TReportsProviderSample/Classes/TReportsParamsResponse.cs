using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Classes
{
  public class TReportsParamsResponse
  {
    [JsonProperty("providerParams")]
    public TReportsProviderParams[] ProviderParams { get; set; }
  }  
}
