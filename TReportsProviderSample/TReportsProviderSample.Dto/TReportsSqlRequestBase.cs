using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Dto
{
  public class TReportsSqlRequestBase : TReportsRequestBase
  {
    [JsonProperty("sqlText")]
    public string SqlText { get; set; }

    [JsonProperty("sqlParameters")]
    public SqlParameter[] SqlParameters { get; set; }
  }

  public partial class SqlParameter
  {
    [JsonProperty("paramName")]
    public string ParamName { get; set; }

    [JsonProperty("paramValue")]
    public object ParamValue { get; set; }

    [JsonProperty("paramType")]
    public string ParamType { get; set; }
  }
}