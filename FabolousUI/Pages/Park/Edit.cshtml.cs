using BussinessLogicLibrary;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.Park
{
    [BindProperties]
    public class EditModel : PageModel
    {


        private readonly IUnitOfWork _contextUnitOfWork;
        JsonEditor MyJsonEditor = new JsonEditor();
        private readonly FabolousDbContext _context;
        public Car MyCar { get; set; } = new Car();
        public Motorcycle MyMc { get; set; } = new Motorcycle();
        public string MyObject { get; set; }
        public IEnumerable<Parkingspot> myNum;
        public ParkingGarage Garage;

        public GarageFunctions GarageFunctions;

        public EditModel(IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;
            GarageFunctions = new GarageFunctions(_contextUnitOfWork);
            myNum = new List<Parkingspot>();
        }
      
        public void OnGet(int id)
        {
            Garage = GarageFunctions.InstanciateGarage();
            Garage = GarageFunctions.GetParkedVehicles(Garage);
            myNum = Garage.spots.Where(x => x.Id == id);
        }
    }
}
