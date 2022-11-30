using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.Model
{
    public class UserVisit
    {
        public string VisitID { get; set; }
        public string DoctorName { get; set; }
        public string FloorLevel { get; set; }
        public string OfficeNumber { get; set; }
        public string SpecializationName { get; set; }
        public DateTime DateAndTime { get; set; }
        public string FacilityName { get; set; }
        public string Date
        {
            get { return DateAndTime.ToString("dd-MM-yyyy"); }
        }
        public string Time
        {
            get { return DateAndTime.ToString("HH:mm"); }
        }       
        public Address Address { get; set; }

        public string FacilityInfoToString()
        {
            return FacilityName + "\n" + Address.City + "\n" + Address.Street + " " + Address.StreetNumber + "\n" + Address.PostCode;
        }
    }
}
