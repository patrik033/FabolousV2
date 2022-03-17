using BussinessLogicLibrary.Models;
using DatabaseAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FabolousUI.Pages.Park
{
    public class CreateBusModel : PageModel
    {
        private readonly FabolousDbContext _context;
        public Bus MyBus { get; set; } = new Bus();
        public List<string> Spots { get; set; }
        public int Id { get; set; }
        public string IncomingSpots { get; set; }
        public string Registration { get; set; }    
        

        public CreateBusModel(FabolousDbContext context)
        {
            _context = context;
        }

        public void OnGet(string currentSpots,string registration)
        {
            IncomingSpots = currentSpots;
            Registration = registration;


        }
        public async Task<IActionResult> OnPost(string currentSpots)
        {
            //kollar efter dubletter
            //var fromCar = _context.cars.Where(x => x.Registration == MyCar.Registration).FirstOrDefault();
            //var fromMc = _context.motorcycles.Where(x => x.Registration == MyCar.Registration).FirstOrDefault();
            //MyBus.Registration = Registration;
            var registration11 = Request.Form["registration"];    
            Spots = currentSpots.Split(',').ToList();
            Spots.RemoveAt(4);
           
            
                foreach (var item in Spots)
                {
                    MyBus = new Bus();
                    MyBus.Registration = registration11;
                    MyBus.Parkingspot = int.Parse(item);
                    await _context.busses.AddAsync(MyBus);
                    await _context.SaveChangesAsync();
                }
                /*if (fromCar == null && fromMc == null)
                {
                    MyBus.Registration = MyBus.Registration.ToUpper();
                    await _context.cars.AddAsync(MyBus);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Car created successfully";
                    return RedirectToPage("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The registration number already exists, please enter a different one");
                }*/
            
            return Page();
        }


    }
}
