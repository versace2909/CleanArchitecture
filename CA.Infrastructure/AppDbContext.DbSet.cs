using CA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CA.Infrastructure;

public partial class AppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Outbox> Outboxes { get; set; }
}