using BussinessLogicLibrary;
using DatabaseAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FabolousUI.Pages.Move
{
    [BindProperties]
    public class UpdateVehicleModel : PageModel
    {
        private readonly FabolousDbContext _context;
        public int MyNewParkingSpot { get; set; }
        public Car UpdatedCar { get; set; }
        public Motorcycle UpdatedMc { get; set; }
        public Bicycle UpdatedBicycle { get; set; }
        public object Holder { get; set; }
        public UpdateVehicleModel(FabolousDbContext context)
        {
            _context = context;
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
                UpdatedCar = await _context.cars.FindAsync(UpdatedCar.Id);
                UpdatedCar.Parkingspot = MyNewParkingSpot;
                await _context.SaveChangesAsync();
            }
            else if (UpdatedCar.Size == 2)
            {
                UpdatedMc = await _context.motorcycles.FindAsync(UpdatedMc.Id);
                UpdatedMc.Parkingspot = MyNewParkingSpot;
                await _context.SaveChangesAsync();
            }
            else if (UpdatedCar.Size == 1)
            {
                UpdatedBicycle = await _context.bicycles.FindAsync(UpdatedBicycle.Id);
                UpdatedBicycle.Parkingspot = MyNewParkingSpot;
                await _context.SaveChangesAsync();
            }
        }
    }
}
