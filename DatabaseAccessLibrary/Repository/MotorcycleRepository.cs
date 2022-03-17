using BussinessLogicLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary.Repository
{
    public class MotorcycleRepository : Repository<Motorcycle>, IMotorcycleRepository
    {

        private readonly FabolousDbContext _context;
        public MotorcycleRepository(FabolousDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Motorcycle motorcycle)
        {
            var myObject = _context.cars.FirstOrDefault(x => x.Id == motorcycle.Id);
            if (myObject != null)
            {
                myObject.Registration = motorcycle.Registration;
            }
        }
    }
}
