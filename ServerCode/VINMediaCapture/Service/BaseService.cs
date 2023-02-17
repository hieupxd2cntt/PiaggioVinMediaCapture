using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Enum;
using VINMediaCaptureEntities.Model;

namespace VINMediaCapture.Service
{
    public class BaseService
    {
        private string _remoteServiceBaseUrl = "";
        private string _authToken = "";
        protected IConfiguration _Configuration;
        private IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public BaseService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _Configuration = configuration;
            _remoteServiceBaseUrl = _Configuration["ConfigApp:UrlApi"];
            var authUname = _Configuration["ConfigApp:UserNameApi"];
            var authPass = _Configuration["ConfigApp:PasswordApi"];
            if (!string.IsNullOrEmpty(authUname))
            {
                var userName = ""; //_session.GetString("UserName");
                var textBytes = Encoding.UTF8.GetBytes(authUname + ":" + authPass + ":" + userName);
                _authToken = Convert.ToBase64String(textBytes);
            }
        }
        public async Task<string> LoadGetApi(string url)
        {
            try
            {
                var userLogin = JsonConvert.DeserializeObject<UserLoginModel>(_httpContextAccessor.HttpContext.Session.GetString(ESession.User.ToString()));
                var user = new Users();
                //user.UserName = _Configuration["ConfigApp:UserNameApi"];
                //user.Password = _Configuration["ConfigApp:PasswordApi"];
                var textBytes = Encoding.UTF8.GetBytes(_Configuration["ConfigApp:UserNameApi"] + ":" + _Configuration["ConfigApp:PasswordApi"] + ":" + user.LoginName);
                _authToken = Convert.ToBase64String(textBytes);
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                   
                    var uri = _remoteServiceBaseUrl + url;
                    var content = "";
                    try
                    {
                        var data = await client.GetAsync(uri);
                        content = await data.Content.ReadAsStringAsync();
                    }
                    catch (Exception e1)
                    {

                        System.Diagnostics.Debug.WriteLine("hieu=" + e1.ToString());
                    }
                    return content;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return string.Empty;
        }
        public async Task<string> PostApi(string url, object o)
        {
            try
            {
                var user = new Users();
              
                var uri = _remoteServiceBaseUrl + url;
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                var userLogin = JsonConvert.DeserializeObject<UserLoginModel>(_httpContextAccessor.HttpContext.Session.GetString(ESession.User.ToString()));
                using (var client = new HttpClient(clientHandler))
                {
                    var textBytes = Encoding.UTF8.GetBytes(_Configuration["ConfigApp:UserNameApi"] + ":" + _Configuration["ConfigApp:PasswordApi"] + ":" + user.LoginName);
                    _authToken = Convert.ToBase64String(textBytes);
                    var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(o, jsonSerializerSettings), System.Text.Encoding.UTF8, "application/json");
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", _authToken);
                    try
                    {
                        var data = await client.SendAsync(requestMessage);
                        var dataString = await data.Content.ReadAsStringAsync();
                        return dataString;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}
