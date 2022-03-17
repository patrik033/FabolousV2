using BussinessLogicLibrary;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUIV2.Pages.Checkout
{
    [BindProperties]
    public class CheckoutBusModel : PageModel
    {
        private readonly IUnitOfWork _contextUnitOfWork;
        public Bus MyBus { get; set; }

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
            MyBus = new Bus();
            _contextUnitOfWork = contextUnitOfWork;
            ParkedTime = new TimeSpan();
            CostCalculator = new CostCalculation();
            Editor = new JsonEditor();
        }
        public void OnGet(int id)
        {

            MyBus = _contextUnitOfWork.Bus.GetFirstOrDefault(x => x.Id == id);
            Cost = Editor.ReadProperty("Bus", "Cost");
            ParkedTime = CostCalculator.ParkedTime(MyBus.StartTime);
            FormatedTime = CostCalculator.ParkedTimeToScreen(MyBus.StartTime);
            TotalCost = CostCalculator.CalculateCost(MyBus.StartTime, int.Parse(Cost));
            FormatedCost = TotalCost.ToString("C2");
            TextString = $"\nParkerad tid:   {FormatedTime} \n" +
                $"Total kostnad:   {FormatedCost}";
        }
        public async Task<IActionResult> OnPostAsync()
        {


            var category = _contextUnitOfWork.Bus.GetAll(x => x.Registration == MyBus.Registration);
            if (category != null)
            {
                _contextUnitOfWork.Bus.RemoveRange(category);
                _contextUnitOfWork.Save();
                TempData["Success"] = "Bus deleted Successfully";
                return RedirectToPage("../Park/Index");
            }
            else
            {
                TempData["error"] = "Vehicle not found";
            }
            return Page();
        }
    }
}
