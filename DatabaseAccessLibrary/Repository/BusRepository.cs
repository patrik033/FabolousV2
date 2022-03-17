using BussinessLogicLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary.Repository
{
    public class BusRepository : Repository<Bus>, IBusRepository
    {
        private readonly FabolousDbContext _context;
        public BusRepository(FabolousDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Bus bus)
        {
            var myObject = _context.busses.Where(x => x.Id == bus.Id).ToList();
            if (myObject != null)
            {
                foreach(var item in myObject)
                {
                    item.Registration = bus.Registration; 
                }
            }
        }
    }
}
