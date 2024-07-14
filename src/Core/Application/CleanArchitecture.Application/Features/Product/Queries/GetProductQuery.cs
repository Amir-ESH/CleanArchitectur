using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.DTO.Product;

namespace CleanArchitecture.Application.Features.Product.Queries;

public class GetProductQuery : IRequest<ResultDto<ProductDto?>>
{
    public long ProductId { get; set; }

    public GetProductQuery(long productId)
    {
        ProductId = productId;
    }

    public GetProductQuery()
    {
        
    }
}
