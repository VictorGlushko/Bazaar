using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bazaar.Models;

namespace Bazaar.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGameRepository Games { get; }
        public IGenreRepository Genres { get; }
        public IModeRepository Mode { get; set; }
        public IPlatformRepository Platform { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Games = new GameRepository(context);
            Genres = new GenreRepository(context);
            Mode = new ModeRepository(context);
            Platform = new PlatformRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}