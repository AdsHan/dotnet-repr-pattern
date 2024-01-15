using ReprPattern.API.Data.Enums;

namespace ReprPattern.API.Data.DomainObjects;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public EntityStatusEnum Status { get; set; }
    public DateTime DateCreateAt { get; private set; }

    protected BaseEntity()
    {
        DateCreateAt = DateTime.Now;
        Status = EntityStatusEnum.Active;
    }
}