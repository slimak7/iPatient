using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.Model
{
    public class DoctorExtended : Doctor
    {
        public string FacilityName { get; set; }
        public Address Address { get; set; }
    }
}
