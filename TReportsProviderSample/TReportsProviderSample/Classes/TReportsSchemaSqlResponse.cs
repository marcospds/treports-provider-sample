using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

using TReportsProviderSample.Classes.Commom;

namespace TReportsProviderSample.Classes
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