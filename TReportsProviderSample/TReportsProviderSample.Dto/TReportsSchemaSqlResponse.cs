using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Dto
{
  public class TReportsSchemaSqlResponse : TReportsResponseBase
  {
    [JsonProperty("schemaSql")]
    public SchemaSql SchemaSql { get; set; }
  }

  public partial class SchemaSql
  {
    [JsonProperty("columns")]
    public Column[] Columns { get; set; }
  }  
}