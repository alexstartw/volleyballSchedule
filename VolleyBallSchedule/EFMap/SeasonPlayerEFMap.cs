using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolleyBallSchedule.Models;

namespace VolleyBallSchedule.EFMap;

public class SeasonPlayerEFMap : IEntityTypeConfiguration<SeasonPlayers>
{
    public void Configure(EntityTypeBuilder<SeasonPlayers> builder)
    {
        builder.ToTable("season_players");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x=>x.Gender)
            .HasColumnName("gender")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.Property(x => x.LineId)
            .HasColumnName("line_uid")
            .IsRequired();
        
        builder.Property(x => x.CreatedTime)
            .HasColumnName("created_time")
            .IsRequired();
    }
}