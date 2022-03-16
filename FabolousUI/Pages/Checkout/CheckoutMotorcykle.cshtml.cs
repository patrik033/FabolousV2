using BussinessLogicLibrary;
using BussinessLogicLibrary.Stuff;
using DatabaseAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.Checkout
{
    public class CheckoutMotorcykleModel : PageModel
    {
        private readonly FabolousDbContext _context;
        public Motorcycle MyCar { get; set; } = new Motorcycle();

        public string FormatedTime { get; set; }
        public TimeSpan ParkedTime { get; set; }
        public JsonEditor Editor { get; set; }
        public CostCalculation CostCalculator { get; set; }
        public string Cost { get; set; }
        public decimal TotalCost { get; set; }
        public string FormatedCost { get; set; }
        public string TextString { get; set; }

        public CheckoutMotorcykleModel(FabolousDbContext context)
        {
            ParkedTime = new TimeSpan();
            _context = context;
            CostCalculator = new CostCalculation();
            Editor = new JsonEditor();
        }

        public void OnGet(int id)
        {
            MyCar = _context.motorcycles.FirstOrDefault(c => c.Id == id);
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

            var category = await _context.motorcycles.FindAsync(id);
            if (category != null)
            {
                _context.motorcycles.Remove(category);
                await _context.SaveChangesAsync();
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