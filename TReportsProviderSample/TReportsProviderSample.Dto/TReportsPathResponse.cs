using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Dto
{
  public class TReportsPathResponse : TReportsResponseBase
  {
    [JsonProperty("paths")]
    public List<Path> Paths { get; set; }
  }

  public class Path
  {
    [JsonProperty("pathName")]
    public string PathName { get; set; }

    [JsonProperty("parentTableName")]
    public string ParentTableName { get; set; }

    [JsonProperty("childTableName")]
    public string ChildTableName { get; set; }

    [JsonProperty("parentColumns")]
    public List<ChildColumnElement> ParentColumns { get; set; }

    [JsonProperty("childColumns")]
    public List<ChildColumnElement> ChildColumns { get; set; }
    
    [JsonProperty("childPaths")]
    public List<Path> ChildPaths { get; set; }
  }
}
