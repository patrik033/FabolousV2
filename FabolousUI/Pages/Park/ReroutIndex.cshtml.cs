using BussinessLogicLibrary;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FabolousUI.Pages.Park
{
    [BindProperties]
    public class ReroutIndexModel : PageModel
    {
        private readonly FabolousDbContext _context;
        [BindProperty(SupportsGet = true)]
        public dynamic MyVehicle { get; set; }

        //page number variable
        [BindProperty(SupportsGet = true)]
        public int P { get; set; } = 1;

        //page size variable
        [BindProperty(SupportsGet = true)]
        public int S { get; set; } = 50;
        public int TotalRecords { get; set; } = 0;


        public ParkingGarage Garage;
        public GarageFunctions GarageFunctions;
        public JsonEditor jsonEditor = new JsonEditor();

        public ReroutIndexModel(FabolousDbContext context)
        {
            _context = context;
            GarageFunctions = new GarageFunctions(_context);
        }
        public IActionResult OnGet(Dictionary<string, string> passedObject)
        {

            if (passedObject.ContainsKey("Motorcycle"))
            {
                if (passedObject.Where(x => x.Key == "Motorcycle").FirstOrDefault().Key.Any() != null)
                {
                    MyVehicle = JsonConvert.DeserializeObject<Motorcycle>(passedObject.Where(x => x.Key == "Motorcycle").FirstOrDefault().Value);
                }
            }
            else if (passedObject.ContainsKey("Car"))
            {
                if (passedObject.Where(x => x.Key == "Car").FirstOrDefault().Key.Any() != null)
                {
                    MyVehicle = JsonConvert.DeserializeObject<Car>(passedObject.Where(x => x.Key == "Car").FirstOrDefault().Value);
                }
            }
            else
                return RedirectToPage("../WrongParkingSpotCount");

            Garage = GarageFunctions.InstanciateGarage();
            Garage = GarageFunctions.GetParkedVehicles(Garage);

            TotalRecords = Garage.spots.Count();

            if (TotalRecords < GarageFunctions.GetHighestParkingSpot())
            {
                return RedirectToPage("../WrongParkingSpotCount");
            }
            Garage.spots = Garage.spots.Skip((P - 1) * S).Take(S).ToList();
            return Page();
        }
    }
}
