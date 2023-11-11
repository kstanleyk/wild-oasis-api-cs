using System;

namespace Core.Common.Core;

public static class PropertySanitizer
{
    public static T TrimWhiteSpaceOnRequest<T>(T obj)
    {
        if (obj != null)
        {
            var properties = obj!.GetType().GetProperties();
            foreach (var property in properties)
            {
                try
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var o = property.GetValue(obj, null) ?? "";
                        var s = (string)o;
                        property.SetValue(obj, s.Trim());
                    }
                    else
                    {
                        //property.SetValue(obj, TrimWhiteSpaceOnRequest(property.GetValue(obj)));
                    }
                }
                catch (Exception)
                {
                    //log.info("Error converting field " + field.getName());
                }
            }

        }
        return obj;
    }
}