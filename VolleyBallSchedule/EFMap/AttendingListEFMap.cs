using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolleyBallSchedule.Models;

namespace VolleyBallSchedule.EFMap;

public class AttendingListEFMap : IEntityTypeConfiguration<AttendingList>
{
    public void Configure(EntityTypeBuilder<AttendingList> builder)
    {
        builder.ToTable("attending_list");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x=>x.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(x => x.LineId)
            .HasColumnName("line_uid");
        
        builder.Property(x => x.InterimId)
            .HasColumnName("interim_id");

        builder.Property(x => x.Gender)
            .HasColumnName("gender")
            .IsRequired();

        builder.Property(x => x.ActivityTime)
            .HasColumnName("activity_time")
            .IsRequired();

        builder.Property(x => x.UpdateTime)
            .HasColumnName("update_time")
            .IsRequired();
    }
}