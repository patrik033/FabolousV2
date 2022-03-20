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

            var category = _contextOfWork.Bus.GetAll(x => x.Registration == tempString);


            foreach (var item in category)
            {
                item.Registration = MyBus.Registration;
                _contextOfWork.Bus.Update(item);
                _contextOfWork.Save();
                TempData["Success"] = "Bus edited successfully";
            }
            return Page();

        }
    }


}
