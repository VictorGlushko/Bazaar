using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bazaar.Domain.Entities;

namespace Bazaar.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OriginalPrice { get; set; }
        public int FinalPrice { get; set; }
        public DateTime DateAdded { get; set; }
        public int Quantity { get; set; }
    }
}