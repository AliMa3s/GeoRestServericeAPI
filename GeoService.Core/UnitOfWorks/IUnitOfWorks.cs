using GeoService.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService.Core.UnitOfWorks {
   public interface IUnitOfWorks {
        IContinentRepository continents { get; }
        Task CommitAsync();
        void Commit();
    }
}
