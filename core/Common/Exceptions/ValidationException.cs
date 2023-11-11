using System;
using System.Collections.Generic;

namespace Core.Common.Exceptions;

public class ValidationException : ApplicationException
{
    public List<string> ValidationErrors { get; set; }

    public ValidationException(List<string> validationErrors)
    {
        ValidationErrors = new List<string>();

        foreach (var validationError in validationErrors)
        {
            ValidationErrors.Add(validationError);
        }
    }
}