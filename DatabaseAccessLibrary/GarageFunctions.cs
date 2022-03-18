using BussinessLogicLibrary;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary.Repository.IRepository;
using System.Linq;

namespace DatabaseAccessLibrary
{
    public class GarageFunctions
    {
        private IUnitOfWork _contextUnitOfWork;
        public JsonEditor Editor { get; set; } = new JsonEditor();
        public GarageFunctions(IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;
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
                    var selectedItem = _contextUnitOfWork.Car.GetFirstOrDefault(x => x.Parkingspot == number);

                    if (selectedItem != null && selectedItem.Size <= spot.Size - spot.CurrentSize)
                    {
                        spot.ParkedVehicles.Add(selectedItem);
                        spot.CurrentSize += 4;
                    }
                }
            }
            foreach (var spot in parkingGarage.spots)
            {
                if (spot.Size > spot.CurrentSize)
                {
                    var number = spot.Id;
                    var selectedItem = _contextUnitOfWork.Motorcycle.GetAll(x => x.Parkingspot == number).Take(2);
                    foreach (var vehicle in selectedItem)
                    {
                        if (vehicle != null && vehicle.Size <= spot.Size - spot.CurrentSize)
                        {
                            spot.ParkedVehicles.Add(vehicle);
                            spot.CurrentSize += 2;
                        }
                    }
                }
            }
            foreach (var spot in parkingGarage.spots)
            {
                if (spot.Size > spot.CurrentSize)
                {
                    var number = spot.Id;
                    var selectedItem = _contextUnitOfWork.Bus.GetAll(bus => bus.Parkingspot == number).Take(4);
                    foreach (var item in selectedItem)
                    {
                        if (item != null)
                        {
                            spot.ParkedVehicles.Add(item);
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
                    var selectedItem = _contextUnitOfWork.Bicycle.GetAll(bike => bike.Parkingspot == number);
                    foreach (var item in selectedItem)
                    {
                        if (item != null)
                        {
                            spot.ParkedVehicles.Add(item);
                            spot.CurrentSize += 1;
                        }
                    }
                }
            }
            return parkingGarage;
        }
        public int GetHighestParkingSpot()
        {
            int Max = 0;
            foreach (var car in _contextUnitOfWork.Car.GetAll())
            {
                if (_contextUnitOfWork.Car != null && car.Parkingspot > Max)
                {
                    Max = car.Parkingspot;
                }
            }
            foreach (var mc in _contextUnitOfWork.Motorcycle.GetAll())
            {
                if (_contextUnitOfWork.Motorcycle != null && mc.Parkingspot > Max)
                {
                    Max = mc.Parkingspot;
                }
            }
            return Max;
        }
    }
}
