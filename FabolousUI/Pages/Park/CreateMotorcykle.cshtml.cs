using BussinessLogicLibrary;
using DatabaseAccessLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUI.Pages.Park
{
    [BindProperties]
    public class CreateMotorcykleModel : PageModel
    {
        private readonly IUnitOfWork _contextUnitOfWork;

        public Motorcycle MyMc { get; set; } = new Motorcycle();
        public int Id { get; set; }

        public CreateMotorcykleModel( IUnitOfWork contextUnitOfWork)
        {
            _contextUnitOfWork = contextUnitOfWork;
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
                var fromCar = _contextUnitOfWork.Car.GetFirstOrDefault(x => x.Registration == MyMc.Registration);
                var fromMc = _contextUnitOfWork.Motorcycle.GetFirstOrDefault(x => x.Registration == MyMc.Registration);

                if (fromCar == null && fromMc == null)
                {
                    MyMc.Registration = MyMc.Registration.ToUpper();
                     _contextUnitOfWork.Motorcycle.Add(MyMc);
                    _contextUnitOfWork.Save();
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
