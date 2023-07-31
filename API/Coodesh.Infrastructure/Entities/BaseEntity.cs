using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Coodesh.Infrastructure.Entities;

public abstract partial class BaseEntity : IEquatable<BaseEntity>
{
    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    [Key]
    public Guid Id { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public void SetCreatedAt(DateTime now)
    {
        CreatedAt = now;
    }

    public void SetUpdatedAt(DateTime now)
    {
        UpdatedAt = now;
    }

    public void SetDeletedAt(DateTime now)
    {
        DeletedAt = now;
    }

    public override bool Equals(object obj)
    {
        if (obj is not BaseEntity)
            return false;

        if (Object.ReferenceEquals(this, obj))
            return true;

        BaseEntity item = (BaseEntity)obj;

        return item.Id == this.Id;
    }

    public bool Equals(BaseEntity other)
    {
        return this.Equals(other as object);
    }

    public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);

    public static bool operator ==(BaseEntity left, BaseEntity right)
    {
        if (Object.Equals(left, null))
            return (Object.Equals(right, null));
        else
            return left.Equals(right);
    }

    public static bool operator !=(BaseEntity left, BaseEntity right)
    {
        return !(left == right);
    }
}
