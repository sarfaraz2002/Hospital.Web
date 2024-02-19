using Hospital.Services;
using Hospital.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private IContactService _contact;

        public ContactController(IContactService contact)
        {
            _contact = contact;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_contact.GetAll(pageNumber, pageSize));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vm = _contact.GetContactById(id);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(ContactViewModel vm)
        {
            _contact.UpdateContact(vm);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ContactViewModel vm)
        {
            _contact.InsertContact(vm);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _contact.DeleteContact(id);
            return RedirectToAction("Index");
        }
    }
}
