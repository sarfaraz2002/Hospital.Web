using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hospital.ViewModel
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public int HospitalInfoId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public HospitalInfo HospitalInfo { get; set; }
        public ContactViewModel()
        {
            
        }
        public ContactViewModel(Contact model)
        {
            Id = model.Id;
            Phone = model.Phone;
            Email = model.Email;
            HospitalInfoId=model.HospitalId;
           HospitalInfo = model.Hospital;
        }
        public Contact ConvertViewModel(ContactViewModel model)
        {
            return new Contact
            {
            Id = model.Id,
            Phone = model.Phone,
            Email = model.Email,
            HospitalId = model.HospitalInfoId,
            Hospital=model.HospitalInfo
        };
        }
    }
}
