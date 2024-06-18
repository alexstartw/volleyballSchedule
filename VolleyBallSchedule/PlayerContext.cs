using System.Data;
using System.Data.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VolleyBallSchedule.Models;

namespace VolleyBallSchedule;

public class PlayerContext : DbContext
{
    public PlayerContext(DbContextOptions<PlayerContext> options) : base(options)
    {
    }
    
    public DbSet<Players> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("public");
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}