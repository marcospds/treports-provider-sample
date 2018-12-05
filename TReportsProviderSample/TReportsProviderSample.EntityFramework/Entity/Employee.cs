using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TReportsProviderSampleEntityFramework
{
  [Description("Table of Employees")]
  public class Employee : Entity
  {
    [Description("Departmento Id")]
    public int DepartmentId { get; set; }

    [Description("Salary Description")]
    [Column(TypeName = "decimal(9,2)")]
    public decimal Salary { get; set; }

    [Description("Birthdate")]
    public DateTime Birthdate { get; set; }

    [Description("Status")]
    public bool Status { get; set; }

    [MaxLength(1)]
    [Description("Sex")]
    public string Sex { get; set; }

    public Department Department { get; set; }
    public ICollection<Dependent> Dependents { get; set; }
    public ICollection<EmployeeSalaryHistory> EmployeeSalaryHistories { get; set; }

  }
}
