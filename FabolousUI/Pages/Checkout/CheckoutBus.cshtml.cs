using BussinessLogicLibrary;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUIV2.Pages.Checkout
{
    public class CheckoutBusModel : PageModel
    {
        private readonly IUnitOfWork _contextUnitOfWork;
        public Bus MyBus { get; set; } = new Bus();

        public string FormatedTime { get; set; }
        public TimeSpan ParkedTime { get; set; }
        public JsonEditor Editor { get; set; }
        public CostCalculation CostCalculator { get; set; }
        public string Cost { get; set; }
        public decimal TotalCost { get; set; }
        public string FormatedCost { get; set; }
        public string TextString { get; set; }
        public string Registration { get; set; }
        public CheckoutBusModel(IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;
            ParkedTime = new TimeSpan();
            CostCalculator = new CostCalculation();
            Editor = new JsonEditor();
        }
        public void OnGet(int id)
        {
            //Registration = regnumber;
            MyBus = _contextUnitOfWork.Bus.GetFirstOrDefault(x => x.Id == id);
            /*Cost = Editor.ReadProperty("Bus", "Cost");
            ParkedTime = CostCalculator.ParkedTime(MyBus.StartTime);
            FormatedTime = CostCalculator.ParkedTimeToScreen(MyBus.StartTime);
            TotalCost = CostCalculator.CalculateCost(MyBus.StartTime, int.Parse(Cost));
            FormatedCost = TotalCost.ToString("C2");
            TextString = $"\nParkerad tid:   {FormatedTime} \n" +
                $"Total kostnad:   {FormatedCost}";*/
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var registration11 = Request.Form["registration"];
            //här behöver listan fyllas på....funkar ej för tillfället
            var category = _contextUnitOfWork.Bus.GetAll(x => x.Registration == registration11);
            if (category == null)
            {
                TempData["error"] = "Vehicle not found";
                return Page();
            }
            foreach (var reg in category)
            {
                if (category != null)
                {
                    _contextUnitOfWork.Bus.Remove(reg);
                    _contextUnitOfWork.Save();
                    
                }
            }
            
            return Page();
        }
    }
}
