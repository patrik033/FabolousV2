using BussinessLogicLibrary;
using DatabaseAccessLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.EditTypes
{
    [BindProperties]
    public class EditMotorcykleModel : PageModel
    {
        private readonly IUnitOfWork _contextIUnitOfWork;
        public Motorcycle MyMc { get; set; } = new Motorcycle();

        public EditMotorcykleModel(IUnitOfWork contextIUnitOfWork)
        {
            _contextIUnitOfWork = contextIUnitOfWork;
        }
        public void OnGet(int id)
        {
            MyMc = _contextIUnitOfWork.Motorcycle.GetFirstOrDefault(x => x.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {

                var fromCar = _contextIUnitOfWork.Car.GetFirstOrDefault(x => x.Registration == MyMc.Registration);
                var fromMc = _contextIUnitOfWork.Motorcycle.GetFirstOrDefault(x => x.Registration == MyMc.Registration);

                if (fromCar == null && fromMc == null)
                {
                    MyMc.Registration = MyMc.Registration.ToUpper();
                    _contextIUnitOfWork.Motorcycle.Update(MyMc);
                    _contextIUnitOfWork.Save();

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
