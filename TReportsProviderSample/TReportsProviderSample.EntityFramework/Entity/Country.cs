using System.Collections.Generic;
using System.ComponentModel;

namespace TReportsProviderSampleEntityFramework
{
  [Description("Table of Departments")]
  public class Country : Entity
  {
    [Description("Initials")]
    public string Initials { get; set; }

    public ICollection<Company> Companies { get; set; }

  }
}
