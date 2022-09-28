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

namespace iPatient.Managers
{
    public static class APIManager
    {
        private const string _apiURL = "https://192.168.0.217:45455/";
        private static string _token;
        private static LoginReq _loginReq;
        private const int _timeout = 20000;
        private static string _userId;
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
                        EmailAddress = registerReq.Email
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
                        EmailAddress = loginReq.EmailAddress
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

        private static async Task<(dynamic Response, string OtherErrors)> HttpPost(string endpoint, string jsonContent)
        {
            string url = _apiURL + endpoint;

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            request.Content = content;

            var devSslHelper = new DevHttpsConnectionHelper(sslPort: 45455);

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
    }
}
