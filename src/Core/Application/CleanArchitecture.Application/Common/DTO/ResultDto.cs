using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.Common.DTO;

public class ResultDto<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
    public T Data { get; set; } = default!;
    public int StatusCode { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ErrorDetail>? Errors { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? MetaData { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PaginationDetails? Pagination { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TransactionDetails? Transaction { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string ResponseType { get; set; } = null!;

    public ResultDto()
    {
        Errors = new List<ErrorDetail>();
        MetaData = new Dictionary<string, object>();
    }

    public ResultDto(T data, string? message, bool? isSuccess, int? statusCode)
    {
        IsSuccess = isSuccess ?? false;
        Message = message ?? "Something went wrong";
        Data = data;
        StatusCode = statusCode ?? 500;
        Errors = new List<ErrorDetail>();
        MetaData = new Dictionary<string, object>();
    }

    public void AddError(string code, string message, string? field = null)
    {
        Errors?.Add(new ErrorDetail {Code = code, Message = message, Field = field});
    }

    public void AddMetaData(string key, object value)
    {
        if (MetaData != null) MetaData[key] = value;
    }

    public void SetPagination(int currentPage, int pageSize, int totalCount)
    {
        Pagination = new PaginationDetails
        {
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public void SetTransactionDetails(string transactionId, string status)
    {
        Transaction = new TransactionDetails
        {
            TransactionId = transactionId,
            Status = status
        };
    }

    public void SetErrorDetails(string code, string message, string? field)
    {
        Errors?.Add(new ErrorDetail
        {
            Code = code,
            Message = message,
            Field = field
        });
    }

    public class ErrorDetail
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Code { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Field { get; set; }
    }

    public class PaginationDetails
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }

    public class TransactionDetails
    {
        public TransactionDetails(string transactionId, string status)
        {
            TransactionId = transactionId;
            Status = status;
        }

        public TransactionDetails()
        {
            
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string TransactionId { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Status { get; set; } = null!;
    }
}
