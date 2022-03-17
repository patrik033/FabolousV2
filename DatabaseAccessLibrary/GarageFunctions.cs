using BussinessLogicLibrary;
using BussinessLogicLibrary.Models;
using BussinessLogicLibrary.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLibrary
{
    public class GarageFunctions
    {
        private readonly FabolousDbContext _context;
        public JsonEditor Editor { get; set; } = new JsonEditor();
        public GarageFunctions(FabolousDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ska skicka tillbaka en tom lista på alla parkeringsplatser som garaget kan använda sig av. Indatan är antal platser som garaget ska ha
        /// </summary>
        /// <param name="garageSize"></param>
        /// <returns></returns>
        public ParkingGarage InstanciateGarage()
        {
            int numbers = int.Parse(Editor.ReadProperty("Parkinggarage", "Size"));

            ParkingGarage garage = new ParkingGarage();

            for (int i = 0; i < numbers; i++)
            {
                garage.spots.Add(new Parkingspot());
            }

            int num = 1;

            foreach (var item in garage.spots)
            {
                item.Id = num;
                num++;
            }
            return garage;
        }
        /// <summary>
        /// Hämtar fordon från databasen och sätter in dom i en redan instansierad lista.o
        /// </summary>
        /// <param name="garage"></param>
        /// <returns></returns>
        ///

        public ParkingGarage GetParkedVehicles(ParkingGarage parkingGarage)
        {
            foreach (var spot in parkingGarage.spots)
            {
                if (spot.Size > spot.CurrentSize)
                {
                    var number = spot.Id;
                    if (_context.cars.Where(car => car.Parkingspot == number).Any())
                    {
                        var selectedItem = _context.cars.Where(car => car.Parkingspot == number).FirstOrDefault();
                        if (selectedItem != null && selectedItem.Size <= spot.Size - spot.CurrentSize)
                        {
                            spot.ParkedVehicles.Add(selectedItem);
                            spot.CurrentSize += 4;
                        }
                    }
                }
            }
            foreach (var spot in parkingGarage.spots)
            {
                if (spot.Size > spot.CurrentSize)
                {
                    var number = spot.Id;

                    if (_context.motorcycles.Where(x => x.Parkingspot == number).Any())
                    {
                        var selectedItem = _context.motorcycles.Where(car => car.Parkingspot == number).Take(2).ToList();
                        foreach (var vehicle in selectedItem)
                        {
                            if (vehicle != null)
                            {
                                spot.ParkedVehicles.Add(vehicle);
                                spot.CurrentSize += 2;
                            }
                        }
                    }
                }
            }
            foreach (var spot in parkingGarage.spots)
            {
                if (spot.Size > spot.CurrentSize)
                {
                    var number = spot.Id;
                    if (_context.busses.Where(car => car.Parkingspot == number).Any())
                    {
                        var selectedItem = _context.busses.Where(bus => bus.Parkingspot == number).FirstOrDefault();
                        if (selectedItem != null && selectedItem.Size > spot.Size - spot.CurrentSize)
                        {
                            spot.ParkedVehicles.Add(selectedItem);
                            spot.CurrentSize += 4;
                        }
                    }
                }
            }
            return parkingGarage;
        }

        public int GetHighestParkingSpot()
        {
            int Max = 0;
            foreach (var car in _context.cars)
            {
                if (_context.cars != null && car.Parkingspot > Max)
                {
                    Max = car.Parkingspot;
                }
            }
            foreach (var mc in _context.motorcycles)
            {
                if (_context.motorcycles != null && mc.Parkingspot > Max)
                {
                    Max = mc.Parkingspot;
                }
            }
            return Max;
        }
    }
}
