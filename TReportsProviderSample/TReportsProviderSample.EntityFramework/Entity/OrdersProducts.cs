using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TReportsProviderSampleEntityFramework
{
  [Description("Table of Order Products")]
  public class OrdersProducts
  {
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    [Column(TypeName = "decimal(9,2)")]
    public decimal Price { get; set; }

    public int Amount { get; set; }


    public Orders Order { get; set; }
    public Product Product { get; set; }

  }
}
