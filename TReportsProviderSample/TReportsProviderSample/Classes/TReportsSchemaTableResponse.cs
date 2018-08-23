﻿using Newtonsoft.Json;
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
}
