using BussinessLogicLibrary;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.Checkout
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _contextUnitOfWork;
        public Car MyCar { get; set; } = new Car();

        public string FormatedTime { get; set; }
        public TimeSpan ParkedTime { get; set; }
        public JsonEditor Editor { get; set; }
        public CostCalculation CostCalculator { get; set; }
        public string Cost { get; set; }
        public decimal TotalCost { get; set; }
        public string FormatedCost { get; set; }
        public string TextString { get; set; }

        public IndexModel(IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;
            ParkedTime = new TimeSpan();
            CostCalculator = new CostCalculation();
            Editor = new JsonEditor();
        }

        public void OnGet(int id)
        {
            MyCar = _contextUnitOfWork.Car.GetFirstOrDefault(x => x.Id == id);
            Cost = Editor.ReadProperty("Car", "Cost");
            ParkedTime = CostCalculator.ParkedTime(MyCar.StartTime);
            FormatedTime = CostCalculator.ParkedTimeToScreen(MyCar.StartTime);
            TotalCost = CostCalculator.CalculateCost(MyCar.StartTime, int.Parse(Cost));
            FormatedCost = TotalCost.ToString("C2");
            TextString = $"\nParkerad tid:   {FormatedTime} \n" +
                $"Total kostnad:   {FormatedCost}";
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var category = _contextUnitOfWork.Car.GetFirstOrDefault( x => x.Id == id); 
            if (category != null)
            {
                _contextUnitOfWork.Car.Remove(category);
                _contextUnitOfWork.Save();
                TempData["Success"] = "Car deleted successfully";
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
