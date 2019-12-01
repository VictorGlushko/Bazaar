using System;
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
        void Complete();

    }
}
