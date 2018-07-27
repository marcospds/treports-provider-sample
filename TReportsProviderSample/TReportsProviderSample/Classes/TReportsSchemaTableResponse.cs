using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TReportsProviderSample.Classes.Commom;

namespace TReportsProviderSample.Classes
{
  public class TReportsSchemaTableResponse : TReportsResponseBase
  {
    [JsonProperty("schemaTable")]
    public SchemaTable SchemaTable { get; set; }

    [JsonProperty("schemaRelations")]
    public SchemaRelation[] SchemaRelation { get; set; }
  }

  public partial class SchemaTable
  {
    [JsonProperty("tableSourceName")]
    public string TableSourceName { get; set; }

    [JsonProperty("tableSourceDescription")]
    public string TableSourceDescription { get; set; }

    [JsonProperty("columns")]
    public Column[] Columns { get; set; }    
  }

  public partial class SchemaRelation
  {
    [JsonProperty("relationName")]
    public string RelationName { get; set; }

    [JsonProperty("parentSourceName")]
    public string ParentSourceName { get; set; }

    [JsonProperty("childSourceName")]
    public string ChildSourceName { get; set; }

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
