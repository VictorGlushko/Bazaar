using Bazaar.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Bazaar.EntityConfigurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(g => g.Name).IsRequired().HasMaxLength(255);
        }
    }
}