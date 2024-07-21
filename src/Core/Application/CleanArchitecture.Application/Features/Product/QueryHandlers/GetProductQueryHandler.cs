using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.DTO.Product;
using CleanArchitecture.Application.Features.Product.Queries;

namespace CleanArchitecture.Application.Features.Product.QueryHandlers;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ResultDto<ProductDto?>>
{
    private readonly IRepository<Domain.Entities.Products.Product, long> _productsRepository;
    private readonly ILogger<GetProductQueryHandler> _logger;

    public GetProductQueryHandler(IRepository<Domain.Entities.Products.Product, long> productsRepository,
                                  ILogger<GetProductQueryHandler> logger)
    {
        _productsRepository = productsRepository;
        _logger = logger;
    }

    public async Task<ResultDto<ProductDto?>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var result = new ResultDto<ProductDto?>();

        var transaction = Guid.NewGuid().ToString().Replace("-", "");

        try
        {
            if (await _productsRepository.GetByIdAsync(cancellationToken, request.ProductId) is { } product)
            {
                result.Data = product.Adapt<ProductDto>();
                result.IsSuccess = true;
                result.Message = "Product information received.";
                result.StatusCode = 200;
                result.SetTransactionDetails(transaction, "Succeed");
            }
            else
            {
                result.IsSuccess = true;
                result.Message = "Product information not found.";
                result.StatusCode = 404;
                result.SetTransactionDetails(transaction, "Not Found");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");

            result.IsSuccess = false;
            result.StatusCode = 400;
            result.Message = "Something went wrong";
            result.ResponseType = "Exception error";
            result.AddError("1400", ex.Message);
            result.SetTransactionDetails(transaction, "Error");
        }

        return result;
    }
}
