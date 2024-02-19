using Hospital.Models;
using Hospital.Repositories.Repository;
using Hospital.Utilities;
using Hospital.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteRoom(int id)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(id);
            _unitOfWork.GenericRepository<Room>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<RoomViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new RoomViewModel();
            int totalCount;

            List<RoomViewModel> vmList = new List<RoomViewModel>();
            try
            {
                int ExculdeRecords = (pageSize * pageNumber) - pageSize;
                var modelList = _unitOfWork.GenericRepository<Room>().GetAll(includeProperties: "Hospital")
                    .Skip(ExculdeRecords).Take(pageSize).ToList();
                totalCount = _unitOfWork.GenericRepository<Room>().GetAll().ToList().Count;
                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<RoomViewModel>
            {
                Data = vmList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalCount
            };
            return result;
        }

        public RoomViewModel GetRoomById(int RoomId)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(RoomId);
            var vm = new RoomViewModel(model);
            return vm;
        }

        public void InsertRoom(RoomViewModel Room)
        {
            var model = new RoomViewModel().ConvertViewModel(Room);
            _unitOfWork.GenericRepository<Room>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateRoom(RoomViewModel Room)
        {
            var model = new RoomViewModel().ConvertViewModel(Room);
            var ModelById = _unitOfWork.GenericRepository<Room>().GetById(model.Id);
            ModelById.RoomNumber = Room.RoomNumber;
            ModelById.Status = Room.Status;
            ModelById.HospitalId = Room.HospitalInfoId;
            ModelById.Type = Room.Type;
            _unitOfWork.GenericRepository<Room>().Update(model);
            _unitOfWork.Save();
        }
        private List<RoomViewModel> ConvertModelToViewModelList(List<Room> list)
        {
            return list.Select(x => new RoomViewModel(x)).ToList();
        }
    }
}
