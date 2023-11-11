﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Common.Core;

public sealed class TypeMapping<TSource, TTarget>
{
    public TSource _source { get; private set; }
    public TTarget _target { get; private set; }

    public List<PropertyInfo> _SourcePropertiesToIgnore { get; private set; }
    public List<PropertyInfo> _TargetPropertiesToIgnore { get; private set; }

    public TypeMapping() : this(Activator.CreateInstance<TTarget>())
    {

    }

    public TypeMapping(TTarget target)
    {
        _target = target;
        _SourcePropertiesToIgnore = new List<PropertyInfo>();
        _TargetPropertiesToIgnore = new List<PropertyInfo>();
    }

    public TypeMapping<TSource, TTarget> IgnoreTargetPropety<T>(Expression<Func<TTarget, T>> propertyExpression)
    {
        if (_TargetPropertiesToIgnore == null)
            _TargetPropertiesToIgnore = new List<PropertyInfo>();

        var propInfo = GetPropertyInfo(propertyExpression, nameof(IgnoreTargetPropety), "tgt");
        _TargetPropertiesToIgnore.Add(propInfo);

        return this;
    }

    public TypeMapping<TSource, TTarget> IgnoreSourcePropety<T>(Expression<Func<TSource, T>> propertyExpression)
    {
        if (_SourcePropertiesToIgnore == null)
            _SourcePropertiesToIgnore = new List<PropertyInfo>();

        var propInfo = GetPropertyInfo(propertyExpression, nameof(IgnoreSourcePropety), "src");
        _SourcePropertiesToIgnore.Add(propInfo);

        return this;
    }

    public TTarget PropertyMap(TSource source)
    {
        _source = source;

        List<PropertyInfo> sourceProperties = _source.GetType().GetProperties().ToList();
        List<PropertyInfo> targetProperties = _target.GetType().GetProperties().ToList();

        //var unmappedTargets = targetProperties.Where(t => ! sourceProperties.Any(s => s.Name == t.Name));
        //var unmappedSources = sourceProperties.Where(s => ! targetProperties.Any(t => t.Name == s.Name));

        //if (unmappedSources.Any() || unmappedTargets.Any())
        //{
        //    var targetStrings = unmappedTargets.Select(x => typeof(TTarget).Name + "." + x.Name);
        //    var srcStrings = unmappedSources.Select(x => typeof(TSource).Name + "." + x.Name);

        //    var exceptionMessage = "Unmapped properties: "
        //        + string.Join(", ", targetStrings.OrderBy(x => x).Concat(srcStrings.OrderBy(x => x)));

        //    throw new Exception(exceptionMessage);
        //}

        foreach (PropertyInfo sourceProperty in sourceProperties)
        {
            PropertyInfo destinationProperty = targetProperties.Find(item => item.Name == sourceProperty.Name);

            if (destinationProperty != null)
            {
                try
                {
                    destinationProperty.SetValue(_target, sourceProperty.GetValue(_source, null), null);
                }
                catch (ArgumentException) { }
            }
        }
        return _target;
    }

    private PropertyInfo GetPropertyInfo<T1, T2>(Expression<Func<T1, T2>> expr, string methodName, string paramName)
    {
        var expression = expr.Body as MemberExpression;
        if (expression != null)
        {
            var propInfo = expression.Member as PropertyInfo;

            if (propInfo != null)
                return propInfo;
        }
        throw new Exception($"{methodName}(...) requires an expression "
                            + $"that is a simple property access of the form '{paramName} => {paramName}.Property'.");
    }
}