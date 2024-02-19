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
    public class ContactService:IContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void DeleteContact(int id)
        {
            var model = _unitOfWork.GenericRepository<Contact>().GetById(id);
            _unitOfWork.GenericRepository<Contact>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<ContactViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new ContactViewModel();
            int totalCount;

            List<ContactViewModel> vmList = new List<ContactViewModel>();
            try
            {
                int ExculdeRecords = (pageSize * pageNumber) - pageSize;
                var modelList = _unitOfWork.GenericRepository<Contact>().GetAll(includeProperties:"Hospital")
                    .Skip(ExculdeRecords).Take(pageSize).ToList();
                totalCount = _unitOfWork.GenericRepository<Contact>().GetAll().ToList().Count;
                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<ContactViewModel>
            {
                Data = vmList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalCount
            };
            return result;
        }

        public ContactViewModel GetContactById(int ContactId)
        {
            var model = _unitOfWork.GenericRepository<Contact>().GetById(ContactId);
            var vm = new ContactViewModel(model);
            return vm;
        }

        public void InsertContact(ContactViewModel Contact)
        {
            var model = new ContactViewModel().ConvertViewModel(Contact);
            _unitOfWork.GenericRepository<Contact>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateContact(ContactViewModel Contact)
        {
            var model = new ContactViewModel().ConvertViewModel(Contact);
            var ModelById = _unitOfWork.GenericRepository<Contact>().GetById(model.Id);
            ModelById.HospitalId = Contact.HospitalInfoId;
            ModelById.Phone = Contact.Phone;
            ModelById.Email = Contact.Email;
            _unitOfWork.GenericRepository<Contact>().Update(model);
            _unitOfWork.Save();
        }
        private List<ContactViewModel> ConvertModelToViewModelList(List<Contact> list)
        {
            return list.Select(x => new ContactViewModel(x)).ToList();
        }
    }
}
