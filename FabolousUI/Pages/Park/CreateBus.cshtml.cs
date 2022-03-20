using BussinessLogicLibrary;
using BussinessLogicLibrary.Models;
using DatabaseAccessLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FabolousUI.Pages.Park
{
    public class CreateBusModel : PageModel
    {
        private readonly IUnitOfWork _contextUnitOfWork;
        public Bus MyBus { get; set; } = new Bus();
        public List<string> Spots { get; set; }
        public int Id { get; set; }
        public string IncomingSpots { get; set; }
        public string Registration { get; set; }

        public CreateBusModel(IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;
        }

        public void OnGet(string currentSpots)
        {
            IncomingSpots = currentSpots;
        }
        public async Task<IActionResult> OnPost(string currentSpots)
        {
            Registration = Request.Form["registration"].ToString().ToUpper();
            Spots = currentSpots.Split(',').ToList();
            Spots.RemoveAt(4);

            if (ModelState.IsValid)
            {
                var fromCar = _contextUnitOfWork.Car.GetFirstOrDefault(x => x.Registration == Registration);
                var fromMc = _contextUnitOfWork.Motorcycle.GetFirstOrDefault(x => x.Registration == Registration);
                var fromBicycle = _contextUnitOfWork.Bicycle.GetFirstOrDefault(x => x.Registration == Registration);
                var fromBus = _contextUnitOfWork.Bus.GetFirstOrDefault(x => x.Registration == Registration);
                if (fromCar == null && fromMc == null && fromBicycle == null && fromBus == null)
                {
                    foreach (var item in Spots)
                    {
                        MyBus = new Bus();
                        MyBus.Registration = Registration;
                        MyBus.Parkingspot = int.Parse(item) + 1;

                        _contextUnitOfWork.Bus.Add(MyBus);
                        _contextUnitOfWork.Save();
                    }
                    TempData["Success"] = "Bus created successfully";
                    return RedirectToPage("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The registration number already exists, please enter a different one");
                }

            }
            return Page();
        }
    }
}
