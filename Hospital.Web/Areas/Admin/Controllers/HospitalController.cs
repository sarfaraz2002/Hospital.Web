using Hospital.Services;
using Hospital.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HospitalController : Controller
    {
        private IHospitalInfoService _hospitalinfo;

        public HospitalController(IHospitalInfoService hospitalinfo)
        {
            _hospitalinfo = hospitalinfo;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_hospitalinfo.GetAll(pageNumber, pageSize));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vm = _hospitalinfo.GetHospitalById(id);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(HospitalInfoViewModel vm)
        {
            _hospitalinfo.UpdateHospitalInfo(vm);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(HospitalInfoViewModel vm)
        {
            _hospitalinfo.InsertHospitalInfo(vm);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _hospitalinfo.DeleteHospitalInfo(id);
            return RedirectToAction("Index");
        }
    }
}
