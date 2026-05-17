using Miroku.Data.Entities;

namespace Miroku.Web.ViewModels;

public abstract class BaseViewModel
{
    protected BaseViewModel()
    {        
    }

    protected BaseViewModel(BaseEntity baseEntity)
    {
        Id = baseEntity.Id;
        DateCreated = baseEntity.DateCreated;
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
}