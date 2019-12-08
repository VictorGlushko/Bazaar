using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bazaar.Domain.Entities;

namespace Bazaar.Repository
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}
