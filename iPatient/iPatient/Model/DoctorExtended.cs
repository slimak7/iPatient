using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.Model
{
    public class DoctorExtended : Doctor
    {
        public Facility Facility { get; set; }

        public bool DisplayFacilityInfo
        {
            get
            {
                return Facility != null;
            }
        }
    }
}
