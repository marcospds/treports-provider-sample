using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Classes
{
  public partial class TReportsSchemaTableRequest : TReportsRequestBase
  {
    [JsonProperty("tableSourceName")]
    public string TableSourceName { get; set; }

    [JsonProperty("tablesSourceGetRelations")]
    public TablesSourceGetRelation[] TablesSourceGetRelations { get; set; }
  }

  public partial class TablesSourceGetRelation
  {
    [JsonProperty("tableSourceName")]
    public string TableSourceName { get; set; }
  }
}
