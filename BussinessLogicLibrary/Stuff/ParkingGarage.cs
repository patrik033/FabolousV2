using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLibrary
{
    public class ParkingGarage
    {
        public List<Parkingspot> spots { get; set; } = new List<Parkingspot>();
        public int Id { get; set; }
    }
}
