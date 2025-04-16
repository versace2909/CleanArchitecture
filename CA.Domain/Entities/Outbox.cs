namespace CA.Domain.Entities;

public class Outbox
{
    public int Id { get; set; }
    public string EventName { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public bool IsProcessed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}