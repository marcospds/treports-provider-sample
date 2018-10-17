using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TReportsProviderSampleEntityFramework
{
  [Description("Table of Products")]
  public class Product : Entity
  {
    [Description("Id of Company")]
    public int CompanyId { get; set; }

    [Description("Code")]
    public string Code { get; set; }

    [Description("Description")]
    public string Description { get; set; }

    [Description("Registration Date")]
    public DateTime RegistrationDate { get; set; }

    [Description("Number of Department")]
    [Column(TypeName = "decimal(9,2)")]
    public decimal Price { get; set; }

    public byte[] Image { get; set; }


    public Company Company { get; set; }
    public ICollection<OrdersProducts> OrdersProducts { get; set; }
  }
}
