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

        public Car MyCar { get; set; } = new Car();
        public Motorcycle MyMc { get; set; } = new Motorcycle();
        public Bus MyBus { get; set; } = new Bus();
        public string MyObject { get; set; }
        public IEnumerable<Parkingspot> myNum;      
       
        public ParkingGarage Garage;

        public GarageFunctions GarageFunctions;
        public int NewId { get; set; }
        public string Spots { get; set; }  
        public bool Bus { get; set; }

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

            var indexListWithParkinSpotsOfValueZero = Garage.spots.Select((x, index ) => new { x.CurrentSize, index  }).Where(x => x.CurrentSize == 0).Where(x => x.index <= 50).ToList();
            List<int> positiveForBusParkingSlots = new List<int>();
            positiveForBusParkingSlots.AddRange(indexListWithParkinSpotsOfValueZero.Select(x => x.index));
            Bus = ValidateParkSequense(positiveForBusParkingSlots);          
            if (Bus == true)
            {
                var perfeCt = positiveForBusParkingSlots.Take(4).ToList();
                foreach (var item in perfeCt)
                {
                    Spots += item.ToString() + ',';
                }
            }
        }
        bool ValidateParkSequense(List<int> positiveForBusParkingSlots)
        {
            var perfeCt = positiveForBusParkingSlots.Take(4).ToList();
            int highest = perfeCt.Max();
            int lowest = perfeCt.Min();
            int j = 0;
            int diff = highest - lowest;

            if (diff == 3)
            {
                return true;
            }
            while (positiveForBusParkingSlots.Count() > 3 && diff != 3)
            {
                positiveForBusParkingSlots.RemoveAt(j);
                perfeCt = positiveForBusParkingSlots.Take(4).ToList();
                highest = perfeCt.Max();
                lowest = perfeCt.Min();
                diff = highest - lowest;
                for (int a = 0; a < perfeCt.Count() - 1; a++)
                {
                    if (diff == 3)
                    {
                        return true;
                    }
                    else if (diff != 3)
                    {
                        break;
                    }
                }
            }

            return false;
        }
    }
}
