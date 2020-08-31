using Bazaar.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Bazaar.EntityConfigurations
{
    public class GameConfiguration : EntityTypeConfiguration<Game>
    {
        public GameConfiguration()
        {

            Property(g => g.Name).IsRequired().HasMaxLength(255);
            Property(g => g.Description).IsRequired().HasMaxLength(4096);
            Property(g => g.Publisher).IsRequired().HasMaxLength(255);
            Property(g => g.Developer).IsRequired().HasMaxLength(255);
            Property(g => g.Language).IsRequired().HasMaxLength(255);
            Property(g => g.ReleaseDate).IsRequired();


        }
    }
}