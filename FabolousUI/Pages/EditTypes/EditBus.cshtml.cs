using BussinessLogicLibrary;
using DatabaseAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FabolousUIV2.Pages.EditTypes
{
    [BindProperties]
    public class EditBusModel : PageModel
    {
        private readonly IUnitOfWork _contextOfWork;
        public Bus MyBus { get; set; } = new Bus();
        public string tempString { get; set; }

        public EditBusModel(IUnitOfWork contextOfWork)
        {
            _contextOfWork = contextOfWork;

        }

        public void OnGet(int id)
        {
            MyBus = _contextOfWork.Bus.GetFirstOrDefault(x => x.Id == id);

        }

        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                tempString = tempString.ToUpper();
                var category = _contextOfWork.Bus.GetAll(x => x.Registration == tempString);

                var fromCar = _contextOfWork.Car.GetFirstOrDefault(x => x.Registration == MyBus.Registration);
                var fromMc = _contextOfWork.Motorcycle.GetFirstOrDefault(x => x.Registration == MyBus.Registration);
                var fromBus = _contextOfWork.Bus.GetFirstOrDefault(x => x.Registration == MyBus.Registration);
                var fromBicycle = _contextOfWork.Bicycle.GetFirstOrDefault(x => x.Registration == MyBus.Registration);
                if (fromCar == null && fromMc == null && fromBus == null && fromBicycle == null)
                {

                    foreach (var item in category)
                    {
                        item.Registration = MyBus.Registration;
                        _contextOfWork.Bus.Update(item);
                        _contextOfWork.Save();
                        TempData["Success"] = "Bus edited successfully";
                    }
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
