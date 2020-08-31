using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bazaar.Domain.Entities;
using Bazaar.Models;
using System.Data.Entity;

namespace Bazaar.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;
        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Game> GetGames()
        {
            return _context.Games.Include(g=>g.Image).Include(g=>g.Genres);
        }

        public Game GetGame(int id)
        {
            return _context.Games.SingleOrDefault(g => g.Id == id);
        }

        public void Add(Game game)
        {
            _context.Games.Add(game);
        }

        public void Remove(Game game)
        {
            _context.Games.Remove(game);
        }
    }
}