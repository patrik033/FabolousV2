using BussinessLogicLibrary;
using DatabaseAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.Park
{
    [BindProperties]
    public class CreateMotorcykleModel : PageModel
    {
        private readonly FabolousDbContext _context;
        public Motorcycle MyMc { get; set; } = new Motorcycle();
        public int Id { get; set; }

        public CreateMotorcykleModel(FabolousDbContext context)
        {
            _context = context;
        }

        public void OnGet(int id)
        {
            Id = id;
        }
        public async Task<IActionResult> OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                //kollar efter dubletter
                var fromCar = _context.cars.Where(x => x.Registration == MyMc.Registration).FirstOrDefault();
                var fromMc = _context.motorcycles.Where(x => x.Registration == MyMc.Registration).FirstOrDefault();
                
                if (fromCar == null && fromMc == null)
                {
                    MyMc.Registration = MyMc.Registration.ToUpper();
                    await _context.motorcycles.AddAsync(MyMc);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Motorcycle created successfully";
                    return RedirectToPage("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"The registration number already exists, please enter a different one");
                }
            }
            return Page();
        }
    }
}
