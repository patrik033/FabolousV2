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

        public void OnGet(string currentSpots, string registration)
        {
            IncomingSpots = currentSpots;
            Registration = registration;
        }
        public async Task<IActionResult> OnPost(string currentSpots)
        {
            var registration11 = Request.Form["registration"];
            Spots = currentSpots.Split(',').ToList();
            Spots.RemoveAt(4);

            if (ModelState.IsValid)
            {
                var fromCar = _contextUnitOfWork.Car.GetFirstOrDefault(x => x.Registration == MyBus.Registration);
                var fromMc = _contextUnitOfWork.Motorcycle.GetFirstOrDefault(x => x.Registration == MyBus.Registration);
                if (fromCar == null && fromMc == null)
                {


                    foreach (var item in Spots)
                    {
                        MyBus = new Bus();
                        MyBus.Registration = registration11;
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
