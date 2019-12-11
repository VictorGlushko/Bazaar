using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bazaar.Domain.Entities;
using Bazaar.Models;

namespace Bazaar.Repository
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly ApplicationDbContext _context;
        public PlatformRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Platform> GetPlatforms()
        {
            return _context.Platforms.ToList();
        }
    }
}