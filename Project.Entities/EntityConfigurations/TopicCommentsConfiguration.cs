using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities.Entities;

namespace Project.Entities.EntityConfigurations
{
    public class TopicCommentsConfiguration : IEntityTypeConfiguration<TopicComment>
    {
        public void Configure(EntityTypeBuilder<TopicComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Comment).IsRequired();

            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}