using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Enum;
using VINMediaCaptureEntities.Model;
using VINMediaCaptureEntities.TaiwainAppModel;

namespace TwainScan.Common
{
    public abstract class ApiMethodBase
    {
        private string _remoteServiceBaseUrl { get; set; }
        private string _authToken { get; set; }
        private string authUname { get; set; }
        private string authPass { get; set; }
        private ConfigModel config = Common.GetConfig();
        private string userName { get; set; }
        public ApiMethodBase()
        {
            _remoteServiceBaseUrl = config.WebApi;
            authUname = ConfigurationManager.AppSettings["UserNameApi"];
            authPass = ConfigurationManager.AppSettings["PasswordApi"];
            if (!string.IsNullOrEmpty(authUname))
            {
                if (CurrentValue.User!=null)
                {
                    userName = CurrentValue.User.User.LoginName;
                }
                
                var textBytes = Encoding.UTF8.GetBytes(authUname + ":" + authPass + ":" + userName);
                _authToken = Convert.ToBase64String(textBytes);
            }
        }
        public async Task<string> LoadGetApi(string url)
        {
            try
            {
                var userLogin =new UserLoginModel();
                var textBytes = Encoding.UTF8.GetBytes(authUname + ":" + authPass + ":" + userName);
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

                        ErrorLog.WriteLog("LoadGetApi",e1.Message);
                    }
                    return content;
                }
            }
            catch (Exception e)
            {
                ErrorLog.WriteLog("LoadGetApi", e.Message);
            }
            return string.Empty;
        }
        public string PostApi(string url, object o)
        {
            try
            {
                var user = new Users();

                var uri = _remoteServiceBaseUrl + url;
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                var userLogin = new UserLoginModel();
                if (url != "User/Login")
                {
                    userLogin = CurrentValue.User;
                }

                using (var client = new HttpClient(clientHandler))
                {
                    var textBytes = Encoding.UTF8.GetBytes(authUname+ ":" + authPass+ ":" + user.LoginName);
                    _authToken = Convert.ToBase64String(textBytes);
                    var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(o, jsonSerializerSettings), System.Text.Encoding.UTF8, "application/json");
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", _authToken);
                    try
                    {
                        var data = client.Send(requestMessage);
                        var dataString = data.Content.ReadAsStringAsync().Result;
                        return dataString;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteLog("PostApi", ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.WriteLog("PostApi", e.Message);
            }
            return null;
        }
    }
}
