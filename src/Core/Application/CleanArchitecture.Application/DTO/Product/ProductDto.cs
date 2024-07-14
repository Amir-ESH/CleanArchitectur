using CleanArchitecture.Application.Common.DTO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.DTO.Product;

public class ProductDto : BaseDto<long>
{
    [StringLength(255), Unicode(false)]
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(3,2)")]
    public decimal Discount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Tax { get; set; }

    [Column(TypeName = "decimal(3,2)")]
    public decimal VAT { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Quantity { get; set; }
}
