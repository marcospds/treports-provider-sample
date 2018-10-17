using System.Collections.Generic;
using System.ComponentModel;

namespace TReportsProviderSampleEntityFramework
{
  [Description("Table of Customers")]
  public class Customer : Entity
  {
    [Description("Id of Company")]
    public int CompanyId { get; set; }

    [Description("Code")]
    public string Code { get; set; }

    [Description("Phone")]
    public string Phone { get; set; }

    public Company Company { get; set; }
    public ICollection<Orders> Orders { get; set; }
  }
}
