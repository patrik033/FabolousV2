using BussinessLogicLibrary;
using DatabaseAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FabolousUI.Pages.Park
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly FabolousDbContext _context;
        public Car MyCar { get; set; } = new Car();
        public int Id { get; set; }

        public CreateModel(FabolousDbContext context)
        {
            _context = context;
        }

        public void OnGet(int id)
        {
            Id = id;
        }

        public async Task<IActionResult> OnPost()
        {


            if (ModelState.IsValid)
            {
                //kollar efter dubletter
                var fromCar = _context.cars.Where(x => x.Registration == MyCar.Registration).FirstOrDefault();
                var fromMc = _context.motorcycles.Where(x => x.Registration == MyCar.Registration).FirstOrDefault();
                
                if (fromCar == null && fromMc == null)
                {
                    MyCar.Registration = MyCar.Registration.ToUpper();
                    await _context.cars.AddAsync(MyCar);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Car created successfully";
                    return RedirectToPage("Index");
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
