using BussinessLogicLibrary;
using DatabaseAccessLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.EditTypes
{
    [BindProperties]
    public class EditVehicleModel : PageModel
    {
        private readonly IUnitOfWork _contextOfWork;
        public Car MyCar { get; set; } = new Car();

        public EditVehicleModel(IUnitOfWork contextOfWork)
        {
            _contextOfWork = contextOfWork;
        }
        public void OnGet(int id)
        {
            MyCar = _contextOfWork.Car.GetFirstOrDefault(x => x.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var fromNewCar = _contextOfWork.Car.GetFirstOrDefault(x => x.Registration == MyCar.Registration);
                var fromNewMc = _contextOfWork.Motorcycle.GetFirstOrDefault(x => x.Registration == MyCar.Registration);
                
                if (fromNewCar == null && fromNewMc == null)
                {
                    MyCar.Registration = MyCar.Registration.ToUpper();
                    _contextOfWork.Car.Update(MyCar);
                    _contextOfWork.Save();
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
