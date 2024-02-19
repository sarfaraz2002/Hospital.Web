using Hospital.Services;
using Hospital.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomController : Controller
    {
        private IRoomService _room;

        public RoomController(IRoomService room)
        {
            _room = room;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_room.GetAll(pageNumber, pageSize));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vm = _room.GetRoomById(id);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(RoomViewModel vm)
        {
            _room.UpdateRoom(vm);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RoomViewModel vm)
        {
            _room.InsertRoom(vm);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _room.DeleteRoom(id);
            return RedirectToAction("Index");
        }
    }
}
