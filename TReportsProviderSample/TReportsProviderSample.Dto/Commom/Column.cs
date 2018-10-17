using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Dto
{
  public partial class Column
  {
    [JsonProperty("columnName")]
    public string ColumnName { get; set; }

    [JsonProperty("columnDescription")]
    public string ColumnDescription { get; set; }

    [JsonProperty("columnType")]
    public string ColumnType { get; set; }
  }
}
