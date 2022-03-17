using BussinessLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary.Repository.IRepository
{
    public interface IBicycleRepository : IRepository<Bicycle>
    {
        void Update(Bicycle bicycle);
    }
}
