using iPatient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.Managers
{
    public static class InstanceManager
    {
        public static StartPageViewModel StartPageViewModel { get; set; }
        public static FacilitiesViewModel FacilitiesViewModel { get; set; }
        public static Dictionaries.Dictionary.UserRoles.Roles CurrentUserRole { get; set; }
    }
}
