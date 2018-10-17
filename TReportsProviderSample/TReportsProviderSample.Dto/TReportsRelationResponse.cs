using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Dto
{
  public class TReportsRelationResponse : TReportsResponseBase
  {
    [JsonProperty("relations")]
    public Relation[] Relations { get; set; }
  }

  public partial class Relation
  {
    [JsonProperty("relationName")]
    public string RelationName { get; set; }

    [JsonProperty("parentTableName")]
    public string ParentTableName { get; set; }

    [JsonProperty("childTableName")]
    public string ChildTableName { get; set; }

    [JsonProperty("parentColumns")]
    public List<ChildColumnElement> ParentColumns { get; set; }

    [JsonProperty("childColumns")]
    public List<ChildColumnElement> ChildColumns { get; set; }
  }

  public partial class ChildColumnElement
  {
    [JsonProperty("columnName")]
    public string ColumnName { get; set; }
  }
}
