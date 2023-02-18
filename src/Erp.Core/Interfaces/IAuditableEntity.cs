namespace Erp.Core.Interfaces;

public interface IAuditableEntity
{
    public DateTime InsertedDateTime { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public string InsertedByUserId { get; set; }
    public string? ModifiedByUserId { get; set; }
}