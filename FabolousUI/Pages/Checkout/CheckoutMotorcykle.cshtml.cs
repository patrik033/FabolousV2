using BussinessLogicLibrary;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.Checkout
{
    public class CheckoutMotorcykleModel : PageModel
    {
        private readonly IUnitOfWork _contextIUnitOfWork;
        public Motorcycle MyCar { get; set; } = new Motorcycle();
        public string FormatedTime { get; set; }
        public TimeSpan ParkedTime { get; set; }
        public JsonEditor Editor { get; set; }
        public CostCalculation CostCalculator { get; set; }
        public string Cost { get; set; }
        public decimal TotalCost { get; set; }
        public string FormatedCost { get; set; }
        public string TextString { get; set; }

        public CheckoutMotorcykleModel(IUnitOfWork contextIUnitOfWork)
        {
            ParkedTime = new TimeSpan();
            _contextIUnitOfWork = contextIUnitOfWork;
            CostCalculator = new CostCalculation();
            Editor = new JsonEditor();
        }

        public void OnGet(int id)
        {
            MyCar =  _contextIUnitOfWork.Motorcycle.GetFirstOrDefault(c => c.Id == id);
            Cost = Editor.ReadProperty("Motorcycle", "Cost");
            ParkedTime = CostCalculator.ParkedTime(MyCar.StartTime);
            FormatedTime = CostCalculator.ParkedTimeToScreen(MyCar.StartTime);
            TotalCost = CostCalculator.CalculateCost(MyCar.StartTime, int.Parse(Cost));
            FormatedCost = TotalCost.ToString("C2");
            TextString = $"\nParkerad tid:   {FormatedTime} \n" +
               $"Total kostnad:   {FormatedCost}";

        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var category =  _contextIUnitOfWork.Motorcycle.GetFirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                _contextIUnitOfWork.Motorcycle.Remove(category);
                _contextIUnitOfWork.Save();
                TempData["Success"] = "Vehicle deleted successfully";
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