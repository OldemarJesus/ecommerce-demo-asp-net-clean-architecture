using System.Diagnostics.CodeAnalysis;

namespace EccomerceDemo.Domain.Primitives;

public abstract class Entity : IEqualityComparer<Entity>
{
    protected Entity() { }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; protected init; }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 397;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj.GetType() != GetType()) return false;
        if (obj is not Entity other) return false;
        return Id == other.Id;
    }

    public bool Equals(Entity? x, Entity? y)
    {
        if (x is null && y is null) return true;
        if (x is null || y is null) return false;
        return x.Id == y.Id;
    }

    public int GetHashCode([DisallowNull] Entity obj)
    {
        return obj.Id.GetHashCode() * 397;
    }
}
