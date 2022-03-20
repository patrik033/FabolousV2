using BussinessLogicLibrary;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.Checkout
{
    public class CheckoutBicycleModel : PageModel
    {
        private readonly IUnitOfWork _contextUnitOfWork;
        public Bicycle MyBicycle { get; set; } = new Bicycle();

        public string FormatedTime { get; set; }
        public TimeSpan ParkedTime { get; set; }
        public JsonEditor Editor { get; set; }
        public CostCalculation CostCalculator { get; set; }
        public string Cost { get; set; }
        public decimal TotalCost { get; set; }
        public string FormatedCost { get; set; }
        public string TextString { get; set; }

        public CheckoutBicycleModel(IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;
            ParkedTime = new TimeSpan();
            CostCalculator = new CostCalculation();
            Editor = new JsonEditor();
        }

        public void OnGet(int id)
        {
            MyBicycle = _contextUnitOfWork.Bicycle.GetFirstOrDefault(x => x.Id == id);
            Cost = Editor.ReadProperty("Bicycle", "Cost");
            ParkedTime = CostCalculator.ParkedTime(MyBicycle.StartTime);
            FormatedTime = CostCalculator.ParkedTimeToScreen(MyBicycle.StartTime);
            TotalCost = CostCalculator.CalculateCost(MyBicycle.StartTime, int.Parse(Cost));
            FormatedCost = TotalCost.ToString("C2");
            TextString = $"\nParkerad tid:   {FormatedTime} \n" +
                $"Total kostnad:   {FormatedCost}";
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var category = _contextUnitOfWork.Bicycle.GetFirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                _contextUnitOfWork.Bicycle.Remove(category);
                _contextUnitOfWork.Save();
                TempData["Success"] = "Bicycle deleted successfully";
                return RedirectToPage("../Park/Index");
            }
            else
            {
                TempData["error"] = "Vehicle not found";
                return Page();
            }
        }
    }
}
