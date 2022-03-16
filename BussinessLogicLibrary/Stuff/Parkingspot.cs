using BussinessLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLibrary
{
    public class Parkingspot
    {
        public int Size { get; set; } = 4;  //Totalkapacitet för platsen
        public int Id { get; set; }
        public int CurrentSize { get; set; } = 0; //nuvarande kapacitet
        public List<Vehicle> ParkedVehicles { get; set; } = new List<Vehicle>();
    }
}
