using System;
using System.Linq;

namespace Core.Common.Core;

public static class PropertyMapper
{
    public static TTarget PropertyMap<TSource, TTarget>(TSource source, TTarget target)
        where TSource : class, new()
        where TTarget : class, new()
    {
        var sourceProperties = source.GetType().GetProperties().ToList();
        var targetProperties = target.GetType().GetProperties().ToList();

        foreach (var sourceProperty in sourceProperties)
        {
            var destinationProperty = targetProperties.Find(item => item.Name == sourceProperty.Name);

            if (destinationProperty == null) continue;
            try
            {
                destinationProperty.SetValue(target, sourceProperty.GetValue(source, null), null);
            }
            catch (ArgumentException) { }
        }
        return target;
    }

    public static TargetType<TTarget> ThatMaps<TTarget>()
    {
        return new TargetType<TTarget>();
    }

    public static TTarget PropertyMapSelective<TSource, TTarget>(TSource source, TTarget target)
        where TSource : class, new()
        where TTarget : class, new()
    {
        foreach (var sourceProp in source.GetType().GetProperties())
        {
            var targetProp = target.GetType().GetProperties().FirstOrDefault(p => p.Name == sourceProp.Name);
            if (targetProp == null || targetProp.GetType().Name != sourceProp.GetType().Name) continue;
            var value = sourceProp.GetValue(source);

            if (value == null) continue;
            var type = value.GetType();
            if (type.Name == nameof(DateTime))
            {
                DateTime date;
                DateTime.TryParse(value.ToString(), out date);

                if (date != DateTime.MinValue)
                    targetProp.SetValue(target, value);
            }
            else
            {
                targetProp.SetValue(target, value);
            }
        }
        return target;
    }

    public static TTarget PropertyMapSelective<TSource, TTarget>(TSource source, TTarget target, string[] excludeProperties)
        where TSource : class, new()
        where TTarget : class, new()
    {
        foreach (var sourceProp in source.GetType().GetProperties())
        {
            var targetProp = target.GetType().GetProperties().FirstOrDefault(p => p.Name == sourceProp.Name);
            if (targetProp == null || targetProp.GetType().Name != sourceProp.GetType().Name) continue;
            var value = sourceProp.GetValue(source);

            if (value == null) continue;
            if (excludeProperties.Contains(sourceProp.Name)) continue;
            var type = value.GetType();
            if (type.Name == nameof(DateTime))
            {
                DateTime date;
                DateTime.TryParse(value.ToString(), out date);

                if (date != DateTime.MinValue)
                    targetProp.SetValue(target, value);
            }
            else
            {
                targetProp.SetValue(target, value);
            }
        }
        return target;
    }
}