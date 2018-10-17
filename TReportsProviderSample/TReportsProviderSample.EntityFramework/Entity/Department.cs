using System.Collections.Generic;
using System.ComponentModel;

namespace TReportsProviderSampleEntityFramework
{
  [Description("Table of Departments")]
  public class Department : Entity
  {
    [Description("Id of Company")]
    public int CompanyId { get; set; }

    public Company Company { get; set; }
    public ICollection<Employee> Employees { get; set; }
  }
}
