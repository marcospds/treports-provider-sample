using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TReportsProviderSampleEntityFramework
{
  public class Entity
  {
    [Description("Primary Key")]
    public int Id { get; set; }

    [Description("Name")]
    public string Name { get; set; }
  }
}
