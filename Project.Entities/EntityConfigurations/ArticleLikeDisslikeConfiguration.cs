using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities.Entities;

namespace Project.Entities.EntityConfigurations
{
    public class ArticleLikeDisslikeConfiguration : IEntityTypeConfiguration<ArticleLikeDisslike>
    {
        public void Configure(EntityTypeBuilder<ArticleLikeDisslike> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.LikeDisslikeType).IsRequired();

            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}