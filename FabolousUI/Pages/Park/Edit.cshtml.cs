using BussinessLogicLibrary;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.Park
{
    [BindProperties]
    public class EditModel : PageModel
    {
        JsonEditor MyJsonEditor = new JsonEditor();
        private readonly FabolousDbContext _context;
        public Car MyCar { get; set; } = new Car();
        public Motorcycle MyMc { get; set; } = new Motorcycle();
        public string MyObject { get; set; }
        public IEnumerable<Parkingspot> myNum;
       
        
        public ParkingGarage Garage;

        public GarageFunctions GarageFunctions;
        public int NewId { get; set; }
        public string Spots { get; set; }
        public bool Bus { get; set; }

        public EditModel(FabolousDbContext context)
        {
            _context = context;
            GarageFunctions = new GarageFunctions(_context);
            myNum = new List<Parkingspot>();
        }
      
        public void OnGet(int id)
        {
            Garage = GarageFunctions.InstanciateGarage();
            Garage = GarageFunctions.GetParkedVehicles(Garage);
            
            myNum = Garage.spots.Where(x => x.Id == id);

            Bus = Test(id).Keys.FirstOrDefault();
            NewId = Test(id).Values.FirstOrDefault();

            if (Bus == true)
            {
                for (int i = 0; i < 4; i++)
                {
                    Spots += (NewId + i).ToString() + ",";
                }
            }

        }
        public Dictionary<bool, int> Test(int id)
        {
            Dictionary<bool, int> result = new Dictionary<bool, int>();
            int BussSpacesOccupied = 4;

            for (int i = 0; i < BussSpacesOccupied; i++)
            {
                if (Garage.spots[id - 1 - i] != null && Garage.spots[id - 1 - i].CurrentSize == 0 && Garage.spots[id - i].CurrentSize == 0 && Garage.spots[id + 1 - i].CurrentSize == 0 && Garage.spots[id + 2 - i].CurrentSize == 0 && Garage.spots[id + 2 - i].Id <= 50)
                {
                    result.Add(true, id - i);
                    return result;
                }

            }
            result.Add(false, -1);
            return result;


        }
    }
}
