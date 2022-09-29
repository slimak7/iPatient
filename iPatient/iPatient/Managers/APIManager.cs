using iPatient.Extensions;
using iPatient.Helpers;
using iPatient.ReqModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using iPatient.Model;
using System.Net.Http.Headers;

namespace iPatient.Managers
{
    public static class APIManager
    {
        private const string _apiURL = "https://192.168.0.217:45455/";
        private static string _token;
        private static LoginReq _loginReq;
        private const int _timeout = 20000;
        private static string _userId;
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

                    _userId = result.Response.userId;

                    return (true, null);
                }
                else if (result.OtherErrors == "")
                {
                    return (false, result.Response.Errors[0].ToString());
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

                    _userId = result.Response.userId;

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
                        UserRole = result.Response.role
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

        private static async Task<(bool ok, string errors)> RefreshToken()
        {
            return await Login(new LoginReq()
            {
                EmailAddress = _loginReq.EmailAddress,
                Password = _loginReq.Password
            });

        }

        private static async Task<(dynamic Response, string OtherErrors)> HttpPost(string endpoint, string jsonContent)
        {
            string url = _apiURL + endpoint;

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            request.Content = content;

            var devSslHelper = new DevHttpsConnectionHelper(sslPort: _port);

            using (var client = devSslHelper.HttpClient)
            {
                client.Timeout = TimeSpan.FromMilliseconds(_timeout);
                
                var response = await client.SendAsync(request);

                if (response == null)
                {
                    return (null, "No response from server");
                }

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
