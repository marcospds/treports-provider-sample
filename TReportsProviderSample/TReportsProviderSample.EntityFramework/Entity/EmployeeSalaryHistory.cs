using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TReportsProviderSampleEntityFramework
{
  [Description("Table of Salary History of Employees")]
  public class EmployeeSalaryHistory : Entity
  {
    [Description("Id of Employee")]
    public int EmployeeId { get; set; }

    [Description("Salary")]
    [Column(TypeName = "decimal(9,2)")]
    public decimal Salary { get; set; }

    [Description("Date of change")]
    public DateTime ChangeDate { get; set; }

    public Employee Employee { get; set; }
  }
}
