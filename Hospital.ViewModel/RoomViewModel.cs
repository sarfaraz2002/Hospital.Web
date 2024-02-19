using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public int HospitalInfoId { get; set; }
        public HospitalInfo HospitalInfo { get; set; }
        public RoomViewModel()
        {
            
        }
        public RoomViewModel(Room Model)
        {     
            Id = Model.Id;
            RoomNumber = Model.RoomNumber;
            Type = Model.Type;
            Status = Model.Status;
            HospitalInfoId = Model.HospitalId;
            HospitalInfo = Model.Hospital;
        }
        public Room ConvertViewModel(RoomViewModel Model)
        {
            return new Room
            {
            Id = Model.Id,
            RoomNumber = Model.RoomNumber,
            Type = Model.Type,
            Status = Model.Status,
            HospitalId = Model.HospitalInfoId,
            Hospital=Model.HospitalInfo
            };
        }
    }
}
