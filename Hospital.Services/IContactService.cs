using Hospital.Utilities;
using Hospital.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IContactService
    {
        PagedResult<ContactViewModel> GetAll(int pageNumber, int pageSize);
        ContactViewModel GetContactById(int ContactId);
        void UpdateContact(ContactViewModel Contact);
        void InsertContact(ContactViewModel Contact);
        void DeleteContact(int id);
    }
}
