using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bazaar.Domain.Entities;
using Bazaar.Models;

namespace Bazaar.Repository
{
    public class ModeRepository : IModeRepository
    {
        private readonly ApplicationDbContext _context;
        public ModeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Mode> GetModes()
        {
            return _context.Modes.ToList();
        }
    }
}