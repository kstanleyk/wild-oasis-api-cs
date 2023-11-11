using System.Collections.Generic;

namespace Core.Logger;

public class CustomException
{
    public string ExceptionName { get; set; }
    public string ModuleName { get; set; }
    public string DeclaringTypeName { get; set; }
    public string TargetSiteName { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
    public List<DictEntry> Data { get; set; }
    public CustomException InnerException { get; set; }

    public CustomException GetBaseError()
    {
        return InnerException != null ? InnerException.GetBaseError() : this;
    }
}

public class DictEntry
{
    public string Key { get; set; }
    public string Value { get; set; }
}