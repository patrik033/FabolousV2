using DatabaseAccessLibrary.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FabolousDbContext _context;
        public UnitOfWork(FabolousDbContext context)
        {
            _context = context;
            Car = new CarRepository(_context);
            Motorcycle = new MotorcycleRepository(_context);
        }

        public ICarRepository Car { get;private set; }
        public IMotorcycleRepository Motorcycle { get;private set; }

        

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
