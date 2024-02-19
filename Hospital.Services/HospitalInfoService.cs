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
    public class HospitalInfoService : IHospitalInfoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HospitalInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteHospitalInfo(int id)
        {
            var model= _unitOfWork.GenericRepository<HospitalInfo>().GetById(id);
            _unitOfWork.GenericRepository<HospitalInfo>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<HospitalInfoViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new HospitalInfoViewModel();
            int totalCount;

            List<HospitalInfoViewModel> vmList = new List<HospitalInfoViewModel>();
            try
            {
                int ExculdeRecords = (pageSize * pageNumber) - pageSize;
                var modelList = _unitOfWork.GenericRepository<HospitalInfo>().GetAll()
                    .Skip(ExculdeRecords).Take(pageSize).ToList();
                totalCount = _unitOfWork.GenericRepository<HospitalInfo>().GetAll().ToList().Count;
                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<HospitalInfoViewModel>
            {
                Data = vmList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalCount
            };
            return result;
        }

        public HospitalInfoViewModel GetHospitalById(int HospitalId)
        {
            var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(HospitalId);
            var vm = new HospitalInfoViewModel(model);
            return vm;
        }

        public void InsertHospitalInfo(HospitalInfoViewModel hospitalinfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalinfo);
            _unitOfWork.GenericRepository<HospitalInfo>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            var ModelById = _unitOfWork.GenericRepository<HospitalInfo>().GetById(model.Id);
            ModelById.Name=hospitalInfo.Name;
            ModelById.City = hospitalInfo.City;
            ModelById.Pincode = hospitalInfo.Pincode;
            ModelById.Country = hospitalInfo.Country;
            _unitOfWork.GenericRepository<HospitalInfo>().Update(model);
            _unitOfWork.Save();
        }

        private List<HospitalInfoViewModel> ConvertModelToViewModelList(List<HospitalInfo> list) 
        { 
            return list.Select(x => new HospitalInfoViewModel(x)).ToList(); 
        }
    }
}
