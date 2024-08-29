using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolleyBallSchedule.Models;

namespace VolleyBallSchedule.EFMap;

public class TempPlayerListEFMap : IEntityTypeConfiguration<TempPlayerList>
{
    public void Configure(EntityTypeBuilder<TempPlayerList> builder)
    {
        builder.ToTable("temp_player");

        builder.HasKey(x=>x.Id);
        
        builder.Property(x=>x.Id)
            .HasColumnName("id")
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();
        
        builder.Property(x=>x.Gender)
            .HasColumnName("gender")
            .IsRequired();
        
        builder.Property(x=>x.InviteLineId)
            .HasColumnName("invite_line_id")
            .IsRequired();

        builder.Property(x => x.UpdatedTime)
            .HasColumnName("updated_time")
            .IsRequired();
    }
}