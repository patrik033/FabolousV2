using BussinessLogicLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUIV2.Pages.EditTypes
{
    [BindProperties]
    public class EditBicycleModel : PageModel
    {
        private readonly IUnitOfWork _contextIUnitOfWork;
        public Bicycle MyBicycle { get; set; } = new Bicycle();

        public EditBicycleModel(IUnitOfWork contextIUnitOfWork)
        {
            _contextIUnitOfWork = contextIUnitOfWork;
        }
        public void OnGet(int id)
        {
            MyBicycle = _contextIUnitOfWork.Bicycle.GetFirstOrDefault(x => x.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {

                var fromCar = _contextIUnitOfWork.Car.GetFirstOrDefault(x => x.Registration == MyBicycle.Registration);
                var fromMc = _contextIUnitOfWork.Motorcycle.GetFirstOrDefault(x => x.Registration == MyBicycle.Registration);

                if (fromCar == null && fromMc == null)
                {
                    MyBicycle.Registration = MyBicycle.Registration.ToUpper();
                    _contextIUnitOfWork.Bicycle.Update(MyBicycle);
                    _contextIUnitOfWork.Save();

                    TempData["Success"] = "Bicycle edited successfully";
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
