using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.Model
{
    public class Doctor
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OfficeNumber { get; set; }
        public string FloorNumber { get; set; }
        public Specialization Specialization { get; set; }

        public string OfficeNumberToString
        {
            get => "Gabinet nr. " + OfficeNumber;
        }

        public string FloorNumberToString
        {
            get => FloorNumber != "0" ? "Piętro nr. " + FloorNumber : "Parter";
        }

    }
}
