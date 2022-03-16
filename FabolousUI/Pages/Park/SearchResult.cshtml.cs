using BussinessLogicLibrary;
using BussinessLogicLibrary.Models;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Newtonsoft.Json;

namespace FabolousUI.Pages.Park
{
    [BindProperties]
    public class SearchResult : PageModel
    {
        private readonly FabolousDbContext _context;

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public ParkingGarage Garage;
        public JsonEditor jsonEditor = new JsonEditor();
        public GarageFunctions GarageFunctions;
        public List<Vehicle> Vehicles = new List<Vehicle>();

        public List<Vehicle> SearchForVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            foreach (var vehicle in _context.cars.Where(x => x.Registration.Contains(Search)))
            {
                vehicles.Add((Vehicle)vehicle);
            }
            foreach (var vehicle in _context.motorcycles.Where(x => x.Registration.Contains(Search)))
            {
                vehicles.Add((Vehicle)vehicle);
            }
            return vehicles;
        }


        public SearchResult(FabolousDbContext context)
        {
            _context = context;
            GarageFunctions = new GarageFunctions(_context);
        }

        public void OnGet()
        {
            Garage = GarageFunctions.InstanciateGarage();
            Garage = GarageFunctions.GetParkedVehicles(Garage);
            Vehicles = SearchForVehicles();
        }

        public void OnPost(Dictionary<string, string> name)
        {

        }
    }
}
