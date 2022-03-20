using BussinessLogicLibrary;
using BussinessLogicLibrary.Models;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.Park
{
    public class CreateBicycleModel : PageModel
    {
        private readonly IUnitOfWork _contextUnitOfWork;
        public Bicycle MyBicycle { get; set; } = new Bicycle();
        public int Id { get; set; }

        public CreateBicycleModel(IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;
        }

        public void OnGet(int id)
        {
            Id = id;
        }

        public async Task<IActionResult> OnPost()
        {
            var registration = Request.Form["registration"];
            var id = Request.Form["Id"];

            if (ModelState.IsValid)
            {
                //kollar efter dubletter
                //TODO lägg till detta för alla typer

                MyBicycle.Registration = registration.ToString().ToUpper();
                var fromNewCar = _contextUnitOfWork.Car.GetFirstOrDefault(x => x.Registration == MyBicycle.Registration);
                var fromNewMc = _contextUnitOfWork.Motorcycle.GetFirstOrDefault(x => x.Registration == MyBicycle.Registration);
                var fromNewBicycle = _contextUnitOfWork.Bicycle.GetFirstOrDefault(x => x.Registration == MyBicycle.Registration);
                var fromNewBus = _contextUnitOfWork.Bus.GetFirstOrDefault(x => x.Registration == MyBicycle.Registration);

                if (fromNewCar == null && fromNewMc == null && fromNewBicycle == null && fromNewBus == null)
                {
                    MyBicycle.Parkingspot = int.Parse(id);
                    _contextUnitOfWork.Bicycle.Add(MyBicycle);
                    _contextUnitOfWork.Save();
                    TempData["Success"] = "Bicycle created successfully";
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
