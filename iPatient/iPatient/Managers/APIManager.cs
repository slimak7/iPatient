using iPatient.Extensions;
using iPatient.Helpers;
using iPatient.ReqModels;
using Newtonsoft.Json;
using System.Text;
using iPatient.Model;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;
using System.Net;
using System.Numerics;

namespace iPatient.Managers
{
    public static class APIManager
    {
        private const string _apiURL = "https://192.168.0.126:45455/";
        private static string _token;
        private static LoginReq _loginReq;
        private const int _timeout = 20000;
        private const int _port = 45455;
        private static bool _refreshTokenAttempt = false;
        public static async Task<(bool OK, string Errors)> Register (RegisterReq registerReq)
        {
            Dictionary<string, string> dict = new Dictionary<string, string> ();

            dict.Add("email", registerReq.Email);
            dict.Add("password", registerReq.Password);
            dict.Add("firstName", registerReq.FirstName);
            dict.Add("lastName", registerReq.LastName);
            dict.Add("phoneNumber", "");
            dict.Add("pesel", "");

            string jsonString = dict.ToJsonString();

            try
            {
                var result = await HttpPost("Auth/Register", jsonString);

                if (result.Response != null && result.Response.success == "True")
                {
                    _loginReq = new LoginReq()
                    {
                        Password = registerReq.Password,
                        EmailAddress = registerReq.Email,
                        Id = result.Response.userID.ToString()
                    };

                    _token = result.Response.token.ToString();


                    return (true, null);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString());
                }
                else
                {
                    return (false, result.OtherErrors);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public static async Task<(bool OK, string Errors)> Login(LoginReq loginReq)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("emailAddress", loginReq.EmailAddress);
            dict.Add("password", loginReq.Password);

            string jsonString = dict.ToJsonString();

            try
            {
                var result = await HttpPost("Auth/Login", jsonString);

                if (result.Response != null && result.Response.success == "True")
                {
                    _loginReq = new LoginReq()
                    {
                        Password = loginReq.Password,
                        EmailAddress = loginReq.EmailAddress,
                        Id = result.Response.userID.ToString()
                    };

                    _token = result.Response.token.ToString();


                    return (true, null);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString());
                }
                else
                {
                    return (false, result.OtherErrors);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }

        }

        public static async Task<(bool ok, string errors, Address address)> GetCurrentUserAddress()
        {
            try
            {
                var result = await HttpGet("UsersInfo/UserAddress/" + _loginReq.Id);

                if (result.Response != null && result.Response.success == "True")
                {
                    Address address = new Address()
                    {
                        ID = result.Response.addressID.ToString(),
                        Street = result.Response.street,
                        StreetNumber = result.Response.streetNumber,
                        City = result.Response.city,
                        PostCode = result.Response.postCode
                    };

                    return (true, null, address);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString(), null);
                }
                else
                {
                    return (false, result.OtherErrors, null);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message, null);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }

        public static async Task<(bool ok, string errors, User user)> GetCurrentUserInfo()
        {
            try
            {
                var result = await HttpGet("UsersInfo/UserInfo/" + _loginReq.Id);

                if (result.Response != null && result.Response.success == "True")
                {
                    User user = new User()
                    {
                        FirstName = result.Response.firstName,
                        LastName = result.Response.lastName,
                        PhoneNumber = result.Response.phoneNumber,
                        Email = result.Response.email,
                        PESEL = result.Response.pesel,
                        UserRole = Dictionaries.Dictionary.UserRoles.GetRole(result.Response.role.ToString())
                    };

                    return (true, null, user);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString(), null);
                }
                else
                {
                    return (false, result.OtherErrors, null);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message, null);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }

        public static async Task<(bool ok, string errors)> UpdateUserInfo(User user)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("userId", _loginReq.Id);
            dict.Add("phoneNumber", user.PhoneNumber);
            dict.Add("pesel", user.PESEL);

            string jsonString = dict.ToJsonString();

            try
            {
                var result = await HttpPost("UsersInfo/UserInfo/Update", jsonString, true);

                if (result.Response != null && result.Response.success == "True")
                {                    
                    return (true, null);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString());
                }
                else
                {
                    return (false, result.OtherErrors);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public static async Task<(bool ok, string errors)> UpdateUserAddress(Address address)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("userID", _loginReq.Id);
            dict.Add("street", address.Street);
            dict.Add("streetNumber", address.StreetNumber);
            dict.Add("city", address.City);
            dict.Add("postCode", address.PostCode);

            string jsonString = dict.ToJsonString();

            try
            {
                var result = await HttpPost("UsersInfo/UserAddress/Update", jsonString, true);

                if (result.Response != null && result.Response.success == "True")
                {
                    return (true, null);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString());
                }
                else
                {
                    return (false, result.OtherErrors);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public static async Task<(bool ok, string errors, ObservableCollection<Facility> facilities)> GetAllEditableFacilities()
        {
            try
            {
                var result = await HttpGet("Facilities/AllFacilities/Edit/GetAll/" + _loginReq.Id);

                if (result.Response != null && result.Response.success == "True")
                {
                    ObservableCollection<Facility> facilities = new ObservableCollection<Facility>();

                    if (result.Response.facilities != null)
                    {
                        for (int i = 0; i < result.Response.facilities.Count; i++)
                        {
                            var facility = result.Response.facilities[i];

                            Facility facilityModel = new Facility()
                            {
                                Id = facility.facilityID,
                                Name = facility.facilityName,
                                Address = new Address()
                                {
                                    ID = facility.addressInfo.addressID,
                                    Street = facility.addressInfo.street,
                                    StreetNumber = facility.addressInfo.streetNumber,
                                    City = facility.addressInfo.city,
                                    PostCode = facility.addressInfo.postCode
                                }
                            };

                            facilities.Add(facilityModel);
                        }
                    }

                    return (true, null, facilities);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString(), null);
                }
                else
                {
                    return (false, result.OtherErrors, null);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message, null);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }

        public static async Task<(bool ok, string errors, ObservableCollection<Facility> facilities)> GetAllFacilitiesByCity(string city)
        {
            try
            {
                var result = await HttpGet("Facilities/AllFacilities/GetAll/" + city);

                if (result.Response != null && result.Response.success == "True")
                {
                    ObservableCollection<Facility> facilities = new ObservableCollection<Facility>();

                    if (result.Response.facilities != null)
                    {
                        for (int i = 0; i < result.Response.facilities.Count; i++)
                        {
                            var facility = result.Response.facilities[i];

                            Facility facilityModel = new Facility()
                            {
                                Id = facility.facilityID,
                                Name = facility.facilityName,
                                Address = new Address()
                                {
                                    ID = facility.addressInfo.addressID,
                                    Street = facility.addressInfo.street,
                                    StreetNumber = facility.addressInfo.streetNumber,
                                    City = facility.addressInfo.city,
                                    PostCode = facility.addressInfo.postCode
                                }
                            };

                            facilities.Add(facilityModel);
                        }
                    }

                    return (true, null, facilities);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString(), null);
                }
                else
                {
                    return (false, result.OtherErrors, null);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message, null);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }

        public static async Task<(bool ok, string errors)> UpdateFacility(Facility facility)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("facilityID", (facility.Id == "") ? Guid.Empty.ToString() : facility.Id);
            dict.Add("userID", _loginReq.Id);
            dict.Add("addressID", (facility.Address.ID == "") ? Guid.Empty.ToString() : facility.Address.ID);
            dict.Add("name", facility.Name);
            dict.Add("streetName", facility.Address.Street);
            dict.Add("streetNumber", facility.Address.StreetNumber);
            dict.Add("city", facility.Address.City);
            dict.Add("postCode", facility.Address.PostCode);

            string jsonString = dict.ToJsonString();

            try
            {
                var result = await HttpPost("Facilities/AllFacilities/Edit/AddUpdate", jsonString, true);

                if (result.Response != null && result.Response.success == "True")
                {
                    return (true, null);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString());
                }
                else
                {
                    return (false, result.OtherErrors);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public static async Task<(bool ok, string errors, List<Doctor> doctors, List<Specialization> specializations)> GetDoctorsAndSpecializations(string FacilityID)
        {
            try
            {
                var result = await HttpGet("Facilities/AllFacilities/GetAllSpecializations");

                List<Specialization> specializations = new List<Specialization>();

                if (result.Response != null && result.Response.success == "True")
                {
                    
                    for (int i = 0; i < result.Response.specializations.Count; i++)
                    {
                        var specialization = result.Response.specializations[i];

                        Specialization spec = new Specialization()
                        {
                            ID = specialization.specializationID,
                            Name = specialization.specializationName
                        };

                        specializations.Add(spec);
                    }

                    var resultDoctors = await HttpGet("Facilities/AllFacilities/" + FacilityID + "/GetAllDoctors");

                    if (resultDoctors.Response != null && resultDoctors.Response.success == "True")
                    {
                        List<Doctor> doctors = new List<Doctor>();

                        if (resultDoctors.Response.doctorInfo != null)
                        {
                            for (int i = 0; i < resultDoctors.Response.doctorInfo.Count; i++)
                            {
                                var doctor = resultDoctors.Response.doctorInfo[i];

                                Doctor doct = new Doctor()
                                {
                                    ID = doctor.doctorID,
                                    Specialization = specializations.Find(x => x.ID == doctor.specializationID.ToString()),
                                    FirstName = doctor.firstName,
                                    LastName = doctor.lastName,
                                    OfficeNumber = doctor.officeNumber,
                                    FloorNumber = doctor.floorNumber
                                };

                                doctors.Add(doct);
                            }
                        }

                        return (true, null, doctors, specializations);
                    }
                    else if (resultDoctors.OtherErrors == "")
                    {
                        return (false, result.Response.errors[0].ToString(), null, null);
                    }
                    else
                    {
                        return (false, result.OtherErrors, null, null);
                    }
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString(), null, null);
                }
                else
                {
                    return (false, result.OtherErrors, null, null);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message, null, null);
            }
            catch (Exception e)
            {
                return (false, e.Message, null, null);
            }
        }

        public static async Task<(bool ok, string errors, List<Specialization> specializations)> GetAllSpecializations()
        {
            try
            {
                var result = await HttpGet("Facilities/AllFacilities/GetAllSpecializations");

                List<Specialization> specializations = new List<Specialization>();

                if (result.Response != null && result.Response.success == "True")
                {
                    for (int i = 0; i < result.Response.specializations.Count; i++)
                    {
                        var specialization = result.Response.specializations[i];

                        Specialization spec = new Specialization()
                        {
                            ID = specialization.specializationID,
                            Name = specialization.specializationName
                        };

                        specializations.Add(spec);
                    }
                    return (true, null, specializations);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString(), null);
                }
                else
                {
                    return (false, result.OtherErrors, null);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message, null);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }

        public static async Task<(bool ok, string errors, DoctorVisitsInfo visitsInfo)> GetDoctorVisitsInfo(string DoctorID, DateTime date)
        {
            try
            {
                var result = await HttpGet("Facilities/AllFacilities/Doctors/Visits/Available/" + DoctorID + "/" + date.ToString("MM-dd-yyyy") + "/" + _loginReq.Id);

                if (result.Response != null && result.Response.success == "True")
                {
                    if (result.Response.startTime == "0")
                    {
                        return (true, null, null);
                    }

                    DoctorVisitsInfo visitsInfo = new DoctorVisitsInfo()
                    {
                        StartTime = result.Response.startTime,
                        EndTime = result.Response.endTime,
                        MinutesPerVisit = result.Response.minutesPerVisit,
                        NotAvailableVisits = new List<string>(),
                        UserVisits = new List<string>()
                    };

                    if (result.Response.notAvailableVisits != null)
                    {
                        for (int i = 0; i < result.Response.notAvailableVisits.Count; i++)
                        {
                            string visit = result.Response.notAvailableVisits[i].ToString();
                            visit = visit.Trim();
                            visitsInfo.NotAvailableVisits.Add(visit);
                        }
                    }

                    if (result.Response.userVisits != null)
                    {
                        for (int i = 0; i < result.Response.userVisits.Count; i++)
                        {
                            string visit = result.Response.userVisits[i].ToString();
                            visit = visit.Trim();
                            visitsInfo.UserVisits.Add(visit);
                        }
                    }

                    return (true, null, visitsInfo);

                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString(), null);
                }
                else
                {
                    return (false, result.OtherErrors, null);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message, null);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }

        public static async Task<(bool ok, string errors, List<DoctorExtended> doctors)> FindDoctors(string FacilityID, string SpecializationID, string city)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("specializationID", (SpecializationID == null || SpecializationID == "") ? Guid.Empty.ToString() : SpecializationID);
            dict.Add("facilityID", (FacilityID == null || FacilityID == "") ? Guid.Empty.ToString() : FacilityID);
            dict.Add("city", city);

            string jsonString = dict.ToJsonString();

            try
            {
                var result = await HttpPost("Facilities/AllFacilities/FindDoctors", jsonString, true);

                if (result.Response != null && result.Response.success == "True")
                {
                    List<DoctorExtended> doctors = new List<DoctorExtended>();
                    List<Facility> facilities = new List<Facility>();

                    if (result.Response.facilities != null)
                    {
                        for (int i = 0; i < result.Response.facilities.Count; i++)
                        {
                            var facility = result.Response.facilities[i];

                            facilities.Add(new Facility()
                            {
                                Id = facility.facilityID,
                                Name = facility.facilityName,
                                Address = new Address()
                                {
                                    Street = facility.addressInfo.street,
                                    StreetNumber = facility.addressInfo.streetNumber,
                                    City = facility.addressInfo.city,
                                    PostCode = facility.addressInfo.postCode
                                }
                            });
                        }
                    }

                    if (result.Response.doctors != null)
                    {
                        for (int i = 0; i < result.Response.doctors.Count; i++)
                        {
                            var doctor = result.Response.doctors[i];

                            doctors.Add(new DoctorExtended()
                            {
                                Facility = facilities.Count > 0 ? facilities.Find(x => x.Id == doctor.facilityID.ToString()) : null,
                                FirstName = doctor.firstName,
                                LastName = doctor.lastName,
                                Specialization = new Specialization()
                                {
                                    ID = doctor.specializationID
                                },
                                FloorNumber = doctor.floorNumber,
                                OfficeNumber = doctor.officeNumber,
                                ID = doctor.doctorID
                            });
                        }
                    }
                    return (true, null, doctors);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString(), null);
                }
                else
                {
                    return (false, result.OtherErrors, null);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message, null);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }

        public static async Task<(bool ok, string errors, VisitReceived visit)> ReceiveVisit(string facilityID)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("userID", _loginReq.Id);
            dict.Add("facilityID", facilityID);

            string jsonString = dict.ToJsonString();

            try
            {
                var result = await HttpPost("Facilities/AllFacilities/Doctors/Visits/Receive", jsonString, true);

                if (result.Response != null && result.Response.success == "True")
                {
                    VisitReceived visit = new VisitReceived()
                    {
                        DoctorName = result.Response.doctorName,
                        SpecializationName = result.Response.specializationName,
                        Time = result.Response.time,
                        FloorLevel = result.Response.floorLevel,
                        VisitID = result.Response.visitID,
                        OfficeNumber = result.Response.officeNumber
                    };

                    return (true, null, visit);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString(), null);
                }
                else
                {
                    return (false, result.OtherErrors, null);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message, null);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }

        public static async Task<(bool ok, string errors)> UpdateDoctor(Doctor doctor, string FacilityID)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("doctorID", (doctor.ID == "") ? Guid.Empty.ToString() : doctor.ID);
            dict.Add("facilityID", FacilityID);
            dict.Add("specializationID", doctor.Specialization.ID);
            dict.Add("firstName", doctor.FirstName);
            dict.Add("lastName", doctor.LastName);
            dict.Add("officeNumber", doctor.OfficeNumber);
            dict.Add("floorNumber", doctor.FloorNumber);

            string jsonString = dict.ToJsonString();

            try
            {
                var result = await HttpPost("Facilities/AllFacilities/Edit/AddUpdateDoctor", jsonString, true);

                if (result.Response != null && result.Response.success == "True")
                {
                    return (true, null);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString());
                }
                else
                {
                    return (false, result.OtherErrors);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public static async Task<(bool ok, string errors)> BookVisit(string DoctorID, string dateTime)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("userID", _loginReq.Id);
            dict.Add("doctorID", DoctorID);
            dict.Add("dateAndTime", dateTime);

            string jsonString = dict.ToJsonString();

            try
            {
                var result = await HttpPost("Facilities/AllFacilities/Doctors/Visits/Book", jsonString, true);

                if (result.Response != null && result.Response.success == "True")
                {
                    return (true, null);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString());
                }
                else
                {
                    return (false, result.OtherErrors);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public static async Task<(bool ok, string errors)> ConfirmReceivedVisit(string VisitID)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("visitID", VisitID);
            
            string jsonString = dict.ToJsonString();

            try
            {
                var result = await HttpPost("Facilities/AllFacilities/Doctors/Visits/Receive/Confirm", jsonString, true);

                if (result.Response != null && result.Response.success == "True")
                {
                    return (true, null);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString());
                }
                else
                {
                    return (false, result.OtherErrors);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public static async Task<(bool ok, string errors, List<UserVisit> visits)> GetAllUserVisits()
        {
            try
            {
                var result = await HttpGet("Facilities/AllFacilities/Visits/GetAll/" + _loginReq.Id);

                if (result.Response != null && result.Response.success == "True")
                {
                    List<UserVisit> visits = new List<UserVisit>();

                    if (result.Response.visits != null)
                    {
                        for (int i = 0; i < result.Response.visits.Count; i++)
                        {
                            var visit = result.Response.visits[i];

                            visits.Add(new UserVisit()
                            {
                                DateAndTime = visit.dateAndTime,
                                FloorLevel = visit.floorLevel,
                                OfficeNumber = visit.officeNumber,
                                DoctorName = visit.doctorName,
                                SpecializationName = visit.specializationName,
                                FacilityName = visit.facilityName,
                                VisitID = visit.visitID,
                                Address = new Address()
                                {
                                    City = visit.address.city,
                                    PostCode = visit.address.postCode,
                                    Street = visit.address.street,
                                    StreetNumber = visit.address.streetNumber
                                }
                            });
                        }
                    }

                    return (true, null, visits);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.errors[0].ToString(), null);
                }
                else
                {
                    return (false, result.OtherErrors, null);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                return (false, e.Message, null);
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }

        private static async Task<(bool ok, string errors)> RefreshToken()
        {
            return await Login(new LoginReq()
            {
                EmailAddress = _loginReq.EmailAddress,
                Password = _loginReq.Password
            });

        }

        private static async Task<(dynamic Response, string OtherErrors)> HttpPost(string endpoint, string jsonContent, bool withAuth = false)
        {
            string url = _apiURL + endpoint;

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            request.Content = content;

            var devSslHelper = new DevHttpsConnectionHelper(sslPort: _port);

            using (var client = devSslHelper.HttpClient)
            {
                client.Timeout = TimeSpan.FromMilliseconds(_timeout);
                
                if (withAuth)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                }

                var response = await client.SendAsync(request);

                if (response == null)
                {
                    return (null, "No response from server");
                }

                if (withAuth)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && !_refreshTokenAttempt)
                    {
                        _refreshTokenAttempt = true;

                        var result = await RefreshToken();

                        if (result.ok)
                        {
                            return await HttpPost(endpoint, jsonContent, withAuth);
                        }

                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        _refreshTokenAttempt = false;
                        return (null, "Unauthorized");                    
                    }
                }

                _refreshTokenAttempt = false;

                var responeJsonString = await response.Content.ReadAsStringAsync();

                dynamic responseJson = JsonConvert.DeserializeObject(responeJsonString);

                return (responseJson, string.Empty);
                
            }
        }

        private static async Task<(dynamic Response, string OtherErrors)>HttpGet(string endpoint)
        {
            var url = _apiURL + endpoint;

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var devSslHelper = new DevHttpsConnectionHelper(sslPort: _port);

            using (var client = devSslHelper.HttpClient)
            {
                client.Timeout = TimeSpan.FromMilliseconds(_timeout);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.SendAsync(request);

                if (response == null)
                {
                    return (null, "No response from server");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && !_refreshTokenAttempt)
                {
                    _refreshTokenAttempt = true;

                    var result = await RefreshToken();

                    if (result.ok)
                    {
                        return await HttpGet(endpoint);
                    }
                    
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _refreshTokenAttempt = false;
                    return (null, "Unauthorized");
                }

                var responeJsonString = await response.Content.ReadAsStringAsync();

                dynamic responseJson = JsonConvert.DeserializeObject(responeJsonString);

                _refreshTokenAttempt = false;

                return (responseJson, string.Empty);

            }
        }
    }
}
