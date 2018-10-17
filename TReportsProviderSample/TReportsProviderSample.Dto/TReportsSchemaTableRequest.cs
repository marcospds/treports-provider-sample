using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Dto
{
  public partial class TReportsSchemaTableRequest : TReportsRequestBase
  {
    [JsonProperty("tableSourceName")]
    public string TableSourceName { get; set; }

  }
}
