using System.ComponentModel.DataAnnotations;

namespace Miroku.Data.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
}
