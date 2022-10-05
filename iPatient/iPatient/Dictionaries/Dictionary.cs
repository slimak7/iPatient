using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.Dictionaries
{
    public static class Dictionary
    {
        public static class UserRoles
        {
            public enum Roles
            {
                Patient, Staff, Doctor
            }

            public static Roles GetRole(string roleName)
            {
                if (roleName == null || roleName == "") throw new Exception("Invalid user role name");

                if (roleName == "Patient")
                {
                    return Roles.Patient;
                }
                if (roleName == "Staff")
                {
                    return Roles.Staff;
                }
                if (roleName == "Doctor")
                {
                    return Roles.Doctor;
                }
                return Roles.Patient;
            }
        }
    }
}
