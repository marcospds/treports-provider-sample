using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TReportsProviderSampleEntityFramework
{
  [Description("Table of Dependent")]
  public class Dependent : Entity
  {
    [Description("Birthdate")]
    public DateTime Birthdate { get; set; }

    [MaxLength(1)]
    [Description("Sex")]
    public string Sex { get; set; }

    [Description("Id of Employee")]
    public int EmployeeId { get; set; }

    public Employee Employee { get; set; }
  }
}
