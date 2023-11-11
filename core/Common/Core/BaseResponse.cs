using System.Collections.Generic;

namespace Core.Common.Core;

public abstract class BaseResponse
{
    protected BaseResponse()
    {
        Success = true;
        ValidationErrors = new List<string>();
        Message = string.Empty;
    }

    protected BaseResponse(string message = null)
    {
        Success = true;
        Message = message;
    }

    protected BaseResponse(string message, bool success)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public List<string> ValidationErrors { get; set; }
}