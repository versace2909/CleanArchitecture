using CA.Domain.Entities;
using CA.Infrastructure.Interceptors;
using CA.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CA.Infrastructure;

public partial class AppDbContext : DbContext
{
    private readonly IMessageQueue _messageQueue;
    public AppDbContext(DbContextOptions<AppDbContext> options, IMessageQueue messageQueue) : base(options)
    {
        _messageQueue = messageQueue;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SaveChangeInterceptor(_messageQueue));
        base.OnConfiguring(optionsBuilder);
    }

}