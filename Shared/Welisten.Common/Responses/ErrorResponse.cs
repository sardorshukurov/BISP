namespace Welisten.Common.Responses;

public class ErrorResponse
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public IEnumerable<ErrorResponseFieldInfo> FieldErrors { get; set; }
}

public class ErrorResponseFieldInfo
{
    public string Code { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}