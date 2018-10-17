using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TReportsProviderSample.Dto
{
  public partial class TReportsProviderParams
  {
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("isPassword")]
    public bool IsPassword { get; set; }
  }
}
