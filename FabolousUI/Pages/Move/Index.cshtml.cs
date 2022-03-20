using BussinessLogicLibrary;
using BussinessLogicLibrary.Models;
using DatabaseAccessLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FabolousUI.Pages.Move
{
    [BindProperties]
    public class UpdateVehicleModel : PageModel
    {

        private readonly IUnitOfWork _contextUnitOfWork;
        public int MyNewParkingSpot { get; set; }
        public Car UpdatedCar { get; set; }
        public Motorcycle UpdatedMc { get; set; }
        public Bicycle UpdatedBicycle { get; set; }
        public object Holder { get; set; }
        public UpdateVehicleModel(IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;

        }

        public async Task OnGet(string currentObject,int id)
        {
            MyNewParkingSpot = id;
            Holder = JsonConvert.DeserializeObject<Car>(currentObject);
            UpdatedCar = JsonConvert.DeserializeObject<Car>(currentObject);
            UpdatedMc = JsonConvert.DeserializeObject<Motorcycle>(currentObject);
            UpdatedBicycle = JsonConvert.DeserializeObject<Bicycle>(currentObject);
            if (UpdatedCar.Size == 4)
            {
                UpdatedCar = _contextUnitOfWork.Car.GetFirstOrDefault(x => x.Id == UpdatedCar.Id);
                UpdatedCar.Parkingspot = MyNewParkingSpot;
                _contextUnitOfWork.Save();
            }
            else if (UpdatedCar.Size == 2)
            {

                UpdatedMc = _contextUnitOfWork.Motorcycle.GetFirstOrDefault(x => x.Id == UpdatedMc.Id);
                UpdatedMc.Parkingspot = MyNewParkingSpot;
                _contextUnitOfWork.Save();
            }
            else if (UpdatedCar.Size == 1)
            {
                UpdatedBicycle = _contextUnitOfWork.Bicycle.GetFirstOrDefault(x => x.Id == UpdatedBicycle.Id);
                UpdatedBicycle.Parkingspot = MyNewParkingSpot;
                _contextUnitOfWork.Save();
            }
        }
    }
}
