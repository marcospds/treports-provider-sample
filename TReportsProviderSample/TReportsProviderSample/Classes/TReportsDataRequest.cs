using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReportsProviderSample.Classes
{
  public class TReportsDataRequest : TReportsRequestBase
  {
    [JsonProperty("sentenceMember")]
    public SentenceMember SentenceMember { get; set; }
  } 

  public partial class SentenceMember : TReportsSqlRequestBase
  {
    [JsonProperty("entityName")]
    public string EntityName { get; set; }

    [JsonProperty("maxRecords")]
    public long MaxRecords { get; set; }
  }
}
