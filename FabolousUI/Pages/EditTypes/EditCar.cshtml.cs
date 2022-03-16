using BussinessLogicLibrary;
using DatabaseAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.EditTypes
{
    [BindProperties]
    public class EditVehicleModel : PageModel
    {
        private readonly FabolousDbContext _context;
        public Car MyCar { get; set; } = new Car();

        public EditVehicleModel(FabolousDbContext context)
        {
            _context = context;
        }
        public void OnGet(int id)
        {
            MyCar = _context.cars.FirstOrDefault(c => c.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var fromCar = _context.cars.Where(x => x.Registration == MyCar.Registration).FirstOrDefault();
                var fromMc = _context.motorcycles.Where(x => x.Registration == MyCar.Registration).FirstOrDefault();
                
                if (fromCar == null && fromMc == null)
                {
                    MyCar.Registration = MyCar.Registration.ToUpper();
                    _context.cars.Update(MyCar);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Car edited successfully";
                    return RedirectToPage("../Park/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The registration number already exists, please enter a different one");
                }
            }
            return Page();
        }
    }
}
