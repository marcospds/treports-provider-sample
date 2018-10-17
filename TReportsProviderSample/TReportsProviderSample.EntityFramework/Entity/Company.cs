using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TReportsProviderSampleEntityFramework
{
  [Description("Table of Companys")]
  public class Company : Entity
  {
    [Description("Address")]
    public string Address { get; set; }

    [Description("Foundation Date")]
    public DateTime FoundationDate { get; set; }

    [Description("Id of Country")]
    public int CountryId { get; set; }

    public Country Country { get; set; }
    public ICollection<Department> Departments { get; set; }

    public ICollection<Customer> Customers { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<Orders> Orders { get; set; }
  }
}
