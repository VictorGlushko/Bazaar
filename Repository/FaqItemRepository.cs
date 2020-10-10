using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Bazaar.Domain.Entities;
using Bazaar.Models;

namespace Bazaar.Repository
{
    public class FaqItemRepository : IFaqItemRepository
    {
        private readonly ApplicationDbContext _context;

        public FaqItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddFaqItem(FaqItem item)
        {
            _context.FaqItems.Add(item);
        }

        public FaqItem GetFaqItem(int id)
        {
            return _context.FaqItems.SingleOrDefault(f => f.FaqItemId == id);

        }

        public IEnumerable<FaqItem> GetFaqItems()
        {
            return _context.FaqItems.ToList();
        }

        public void UpdateFaqItem(int id, FaqItem item)
        {
            var faqItemInDb = _context.FaqItems.SingleOrDefault(f => f.FaqItemId == id);

            faqItemInDb.Question = item.Question;
            faqItemInDb.Answer = item.Answer;
            faqItemInDb.Order = item.Order;

        }

        public void DeleteFaqItem(int id)
        {
            var faqItem = _context.FaqItems.SingleOrDefault(f => f.FaqItemId == id);
            _context.FaqItems.Remove(faqItem);
        }
    }
}