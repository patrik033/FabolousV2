using BussinessLogicLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary.Repository
{
    internal class CarRepository : Repository<Car>,ICarRepository
    {
        private readonly FabolousDbContext _context;
        public CarRepository(FabolousDbContext context) :base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Car car)
        {
            var myObject = _context.cars.FirstOrDefault(x => x.Id == car.Id);
            if (myObject != null)
            {
                myObject.Registration = car.Registration;
            }
        }
    }
}
