namespace Comments.SharedKernel;

public abstract class EntityBase
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; } = null;
    protected void Touch() => UpdatedAt = DateTime.UtcNow;
}
