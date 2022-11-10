using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.Model
{
    public class VisitReceived 
    {
        public string VisitID  { get; set; }
        public string DoctorName { get; set; }
        public string FloorLevel { get; set; }
        public string OfficeNumber { get; set; }
        public string SpecializationName { get; set; }
        public string Time { get; set; }
    }
}
