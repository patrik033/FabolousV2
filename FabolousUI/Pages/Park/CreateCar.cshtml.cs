using BussinessLogicLibrary;
using DatabaseAccessLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FabolousUI.Pages.Park
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _contextUnitOfWork;
        public Car MyCar { get; set; } = new Car();
        public int Id { get; set; }

        public CreateModel(IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;
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
                //TODO lägg till detta för alla typer

                var fromNewCar = _contextUnitOfWork.Car.GetFirstOrDefault(x => x.Registration == MyCar.Registration);
                var fromNewMc = _contextUnitOfWork.Motorcycle.GetFirstOrDefault(x => x.Registration == MyCar.Registration);
                if (fromNewCar == null && fromNewMc == null)
                {
                    MyCar.Registration = MyCar.Registration.ToUpper();
                    _contextUnitOfWork.Car.Add(MyCar);
                    _contextUnitOfWork.Save();
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
