namespace Core.Common.Core;

public sealed class TargetType<TTarget>
{
    public TypeMapping<TSource, TTarget> From<TSource>()
    {
        return new TypeMapping<TSource, TTarget>();
    }
}