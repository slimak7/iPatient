using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.Model
{
    public class DoctorVisitsInfo
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MinutesPerVisit { get; set; }
        public List<string> NotAvailableVisits { get; set; }
    }
}
