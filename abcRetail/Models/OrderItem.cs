#nullable disable
using abcRetail;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abcRetail.Models;

public partial class OrderItem
{
    [Key]
    public int OrderItemID { get; set; }

    public int OrderID { get; set; }

    public int ProductID { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(29, 2)")]
    public decimal? Total { get; set; }

    [ForeignKey("OrderID")]
    public virtual Order Order { get; set; }

    [ForeignKey("ProductID")]
    [InverseProperty("OrderItems")]

    public virtual Product Product { get; set; }
}