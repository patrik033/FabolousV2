using BussinessLogicLibrary;
using DatabaseAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.EditTypes
{
    [BindProperties]
    public class EditMotorcykleModel : PageModel
    {
        private readonly FabolousDbContext _context;
        public Motorcycle MyMc { get; set; } = new Motorcycle();

        public EditMotorcykleModel(FabolousDbContext context)
        {
            _context = context;
        }
        public void OnGet(int id)
        {
            MyMc = _context.motorcycles.FirstOrDefault(c => c.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var fromCar = _context.cars.Where(x => x.Registration == MyMc.Registration).FirstOrDefault();
                var fromMc = _context.motorcycles.Where(x => x.Registration == MyMc.Registration).FirstOrDefault();
                
                if (fromCar == null && fromMc == null)
                {
                    MyMc.Registration = MyMc.Registration.ToUpper();
                    _context.motorcycles.Update(MyMc);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Motorcykle edited successfully";
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
