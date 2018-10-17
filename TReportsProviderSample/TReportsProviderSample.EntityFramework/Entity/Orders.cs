using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TReportsProviderSampleEntityFramework
{
  [Description("Table of Orders")]
  public class Orders
  {
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public int CustomerId { get; set; }

    public DateTime Date { get; set; }


    public Company Company { get; set; }
    public Customer Customer { get; set; }
    public ICollection<OrdersProducts> OrdersProducts { get; set; }

  }
}
