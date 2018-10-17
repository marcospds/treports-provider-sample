using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Dto
{
  public class TReportsParamsResponse
  {
    [JsonProperty("providerParams")]
    public TReportsProviderParams[] ProviderParams { get; set; }
  }  
}
