using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Classes
{
  public class TReportsDataReponse : TReportsResponseBase
  {
    [JsonProperty("data")]
    public string Data { get; set; }
  }
}
