using BussinessLogicLibrary;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FabolousUIV2.Pages.Move
{
    public class MoveBusModel : PageModel
    {
        private readonly IUnitOfWork _contextUnitOfWork;
        JsonEditor MyJsonEditor = new JsonEditor();

        [BindProperty]
        public Bus MyBus { get; set; } 
        public string MyObject { get; set; }
        public IEnumerable<Parkingspot> myNum;

        public ParkingGarage Garage;

        public GarageFunctions GarageFunctions;
         
        public List<int> PositiveSpotsForBusMove { get; set; }
        [BindProperty]
        public string SelectedParkingChunk { get; set; }
        public List<SelectListItem> Options { get; set; }
        [BindProperty]
        public string Registration { get; set; }

        public MoveBusModel(IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;
            GarageFunctions = new GarageFunctions(_contextUnitOfWork);
            myNum = new List<Parkingspot>();
        }

        public void OnGet(string Bus, int id)
        {
            Garage = GarageFunctions.InstanciateGarage();
            Garage = GarageFunctions.GetParkedVehicles(Garage);
            this.MyBus = JsonConvert.DeserializeObject<Bus>(Bus);
            Registration = MyBus.Registration;

            var indexListWithParkinSpotsOfValueZero = Garage.spots.Select((x, index) => new { x.CurrentSize, index }).Where(x => x.CurrentSize == 0).Where(x => x.index <= 50).ToList();
            List<int> positiveForBusParkingSlots = new List<int>();
            positiveForBusParkingSlots.AddRange(indexListWithParkinSpotsOfValueZero.Select(x => x.index));
            PositiveSpotsForBusMove = ValidateParkSequense(positiveForBusParkingSlots);

            Options = PositiveSpotsForBusMove.Chunk(4).Select(x => new SelectListItem
            {
                Value = String.Join(" ",x),
                Text = String.Join(" ", x)
            }).ToList();
        }
        public async Task<IActionResult> OnPost()
        {           
            var myBus = MyBus.Registration;
            var selectedParkingId = this.SelectedParkingChunk;
            selectedParkingId = selectedParkingId.Replace(' ', ',');
            var selectedParkingSequence = selectedParkingId.Split(',');
            int spot = int.Parse(selectedParkingSequence[0]) + 1;
            var findBus = _contextUnitOfWork.Bus.GetAll(x => x.Registration == myBus).ToList();

            foreach (var bus in findBus)
            {
                    bus.Parkingspot = spot;
                    _contextUnitOfWork.Save();
                    spot++;
            }

            return RedirectToPage("../Park/Index");
        }
        List<int> ValidateParkSequense(List<int> positiveForBusParkingSlots)
        {
            List<int> result = new List<int>(); 
            var perfeCt = positiveForBusParkingSlots.Take(4).ToList();
            int highest = perfeCt.Max();
            int lowest = perfeCt.Min();
            int j = 0;
            int diff = highest - lowest;

            if (diff == 3)
            {
                result.AddRange(perfeCt);
            }
            while (positiveForBusParkingSlots.Count() > 3 && diff != 3 || positiveForBusParkingSlots.Count() > 3 && diff == 3)
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
                        if (positiveForBusParkingSlots.Count() < 3) { break; }
                        else if(positiveForBusParkingSlots.Count() >= 4)
                        {
                            result.AddRange(perfeCt);
                            positiveForBusParkingSlots.RemoveAt(0);
                            positiveForBusParkingSlots.RemoveAt(1);
                            positiveForBusParkingSlots.RemoveAt(2);                          
                            break;
                        }                      
                    }
                    else if (diff != 3)
                    {
                        break;
                    }
                }
            }
            return result;
        }
    }
}
