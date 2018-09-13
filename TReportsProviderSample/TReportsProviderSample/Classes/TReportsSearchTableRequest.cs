using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Classes
{
  public partial class TReportsSearchTableRequest : TReportsRequestBase
  {
    [JsonProperty("findTable")]
    public string FindTable { get; set; }

  }
}
