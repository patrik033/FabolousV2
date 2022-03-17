using BussinessLogicLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary.Repository.IRepository
{
    public interface IMotorcycleRepository : IRepository<Motorcycle>
    {
        void Update(Motorcycle motorcycle);
    }
}
