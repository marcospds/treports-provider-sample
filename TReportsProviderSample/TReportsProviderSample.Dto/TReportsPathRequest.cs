using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Dto
{
  public class TReportsPathRequest : TReportsRequestBase
  {

    [JsonProperty("tableName")]
    public string TableName { get; set; }

    [JsonProperty("targetTableName")]
    public string TargetTableName { get; set; }
  }
}
