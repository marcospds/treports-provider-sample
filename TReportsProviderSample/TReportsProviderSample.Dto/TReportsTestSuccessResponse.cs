using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Dto
{
  public class TReportsTestSuccessResponse : TReportsResponseBase
  {
    [JsonProperty("testSuccess")]
    public bool Success { get; set; } = true;
  }
}
