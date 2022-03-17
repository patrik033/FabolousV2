using BussinessLogicLibrary;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.Park
{
    [BindProperties]
    public class MapModel : PageModel
    {
        private readonly FabolousDbContext _context;
        public IList<Parkingspot> Cars { get; set; }
        private readonly IUnitOfWork _contextUnitOfWork;

        //page number variable
        [BindProperty(SupportsGet = true)]
        public int P { get; set; } = 1;

        //page size variable
        [BindProperty(SupportsGet = true)]

        public int S { get; set; }
        public int TotalRecords { get; set; } = 0;
        public int StoredVehicles { get; set; }

        public ParkingGarage Garage;
        public GarageFunctions GarageFunctions;
        public JsonEditor jsonEditor = new JsonEditor();

        public MapModel(IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;
            S = int.Parse(jsonEditor.ReadProperty("Parkinggarage", "Size"));
            GarageFunctions = new GarageFunctions(_contextUnitOfWork);
        }

        public IActionResult OnGet()
        {
            Garage = GarageFunctions.InstanciateGarage();
            Garage = GarageFunctions.GetParkedVehicles(Garage);
            TotalRecords = Garage.spots.Count();

            if (TotalRecords < GarageFunctions.GetHighestParkingSpot())
            {
                return RedirectToPage("../WrongParkingSpotCount");
            }

            Garage.spots = Garage.spots.Skip((P - 1) * S).Take(S).ToList();

            Cars = Garage.spots
                .Skip((P - 1) * S)
                .Take(S)
                .ToList();
            return Page();
        }
    }
}
