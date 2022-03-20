using BussinessLogicLibrary;
using BussinessLogicLibrary.Models;
using DatabaseAccessLibrary.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary.Repository
{


    public class BicycleRepository : Repository<Bicycle>, IBicycleRepository
    {
        private readonly FabolousDbContext _context;
        public BicycleRepository(FabolousDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }



        public void Update(Bicycle bicycle)
        {
            var myObject = _context.bicycles.FirstOrDefault(x => x.Id == bicycle.Id);
            if (myObject != null)
            {
                myObject.Registration = bicycle.Registration;
            }
        }

        
    }
}
