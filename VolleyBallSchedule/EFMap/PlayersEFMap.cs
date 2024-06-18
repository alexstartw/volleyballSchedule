using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolleyBallSchedule.Models;

namespace VolleyBallSchedule.EFMap;

public class PlayersEFMap : IEntityTypeConfiguration<Players>
{
    public void Configure(EntityTypeBuilder<Players> builder)
    {
        builder.ToTable("players");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.NickName)
            .HasColumnName("nickname")
            .HasMaxLength(128)
            .IsRequired();
    }
}