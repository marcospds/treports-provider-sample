using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Dto
{
  public partial class TReportsSearchTableRequest : TReportsRequestBase
  {
    [JsonProperty("findTable")]
    public string FindTable { get; set; }

    [JsonProperty("page")]
    public int page { get; set; }

    [JsonProperty("pageSize")]
    public int pageSize { get; set; }
  }
}
