using BussinessLogicLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary.Repository.IRepository
{
    public interface IBusRepository : IRepository<Bus>
    {
        void Update(Bus bus);
    }
}
