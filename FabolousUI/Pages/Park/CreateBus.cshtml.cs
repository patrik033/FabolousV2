using BussinessLogicLibrary;
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
            var registration11 = Request.Form["registration"];
            Spots = currentSpots.Split(',').ToList();
            Spots.RemoveAt(4);                     
                foreach (var item in Spots)
                {
                    MyBus = new Bus();
                    MyBus.Registration = registration11;
                    MyBus.Parkingspot = int.Parse(item)+1;
                    await _context.busses.AddAsync(MyBus);
                    await _context.SaveChangesAsync();
                }
            return Page();
        }
    }
}
