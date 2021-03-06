﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bazaar.Models;

namespace Bazaar.Repository
{
    public interface IUnitOfWork
    {
        IGameRepository Games { get; }
        IGenreRepository Genres { get; }
        IModeRepository Mode { get;  }
        IPlatformRepository Platform { get; }
        IFaqItemRepository FaqItem { get; }
        ICarouselSlideRepository CarouselSlides { get; }
        void Complete();

    }
}
