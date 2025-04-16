namespace CA.Domain.Entities;

public class BaseEntity<T>
{
    public T Id { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string UpdatedBy { get; set; } = string.Empty;
    public string DeletedBy { get; set; } = string.Empty;
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedDate { get; set; }
}