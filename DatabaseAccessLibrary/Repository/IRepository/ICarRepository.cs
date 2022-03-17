using BussinessLogicLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary.Repository.IRepository
{
    public interface ICarRepository : IRepository<Car>
    {
        void Update(Car car);
    }
}
