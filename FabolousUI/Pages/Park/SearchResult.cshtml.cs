using BussinessLogicLibrary;
using BussinessLogicLibrary.Models;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Newtonsoft.Json;

namespace FabolousUI.Pages.Park
{
    [BindProperties]
    public class SearchResult : PageModel
    {
        private readonly IUnitOfWork _contextUnitOfWork;
        private readonly FabolousDbContext _context;

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public ParkingGarage Garage;
        public JsonEditor jsonEditor = new JsonEditor();
        public GarageFunctions GarageFunctions;
        public List<Vehicle> Vehicles = new List<Vehicle>();

        public SearchResult(IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;
            GarageFunctions = new GarageFunctions(_contextUnitOfWork);
        }
       

        public List<Vehicle> SearchForVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            var fada = _contextUnitOfWork.Car.GetAll(x => x.Registration.Contains(Search));

            foreach (var vehicle in _contextUnitOfWork.Car.GetAll(x => x.Registration.Contains(Search)))
            {
                vehicles.Add((Vehicle)vehicle);
            }
            foreach (var vehicle in _contextUnitOfWork.Motorcycle.GetAll(x => x.Registration.Contains(Search)))
            {
                vehicles.Add((Vehicle)vehicle);
            }
            return vehicles;
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
