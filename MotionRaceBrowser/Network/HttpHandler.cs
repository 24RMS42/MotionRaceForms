using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MotionRaceBrowser.Constant;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace MotionRaceBrowser.Network
{
    public class HttpHandler
    {
        private HttpClient httpClient;

        public HttpHandler()
        {
            httpClient = new HttpClient();
        }

        #region LoginAsync
        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Logging in...");
                httpClient.BaseAddress = new Uri(Constants.BASE);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestUri = "?fbrun=" + email + "&fbrpw=" + password + "&applicationid=" + Constants.ApplicaitonId.ToLower();
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                var response = await httpClient.SendAsync(request);

                UserDialogs.Instance.HideLoading();

                var result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("secret result:" + result);

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonArray = JToken.Parse(result);
                    bool success = jsonArray["success"].Value<bool>();
                    if (success)
                    {
                        var loginId = jsonArray["loginid"].ToString();
                        var loginSecret = jsonArray["loginsecret"].ToString();
                        var baseUrl = jsonArray["baseurl"].ToString();
                        baseUrl = baseUrl + "/";

                        HMACSHA1 hashAlgorithm = new HMACSHA1();
                        hashAlgorithm.Key = Encoding.ASCII.GetBytes(Constants.ApplicaitonSecret.ToLower());
                        byte[] bytes = Encoding.ASCII.GetBytes(loginSecret);
                        byte[] hashedBytes = hashAlgorithm.ComputeHash(bytes);
                        string hashedLoginSecret = Convert.ToBase64String(hashedBytes);

                        App.LoginId = loginId;
                        App.HashedSecret = hashedLoginSecret;
                        App.BaseUrl = baseUrl;

                        IDictionary<string, object> properties = Application.Current.Properties;
                        properties["baseUrl"] = baseUrl.Trim();
                        properties["loginId"] = loginId.Trim();
                        properties["hashedSecret"] = hashedLoginSecret.Trim();
                    }
                    else
                    {
                        await HandleError("Invalid user name or password");
                        return false;
                    }

                    return true;
                }
                else
                {
                    await HandleError("Login Failed");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                await HandleError("Login Failed");
                return false;
            }
        }
        #endregion

        #region LoginAsync
        public async Task<bool> AuthUserAsync(string loginId, string loginSecret, string baseUrl)
        {
            try
            {
                var deviceLanguage = System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag;

                HMACSHA1 hashAlgorithm = new HMACSHA1();
                hashAlgorithm.Key = Encoding.ASCII.GetBytes(Constants.ApplicaitonSecret.ToLower());
                byte[] bytes = Encoding.ASCII.GetBytes(loginSecret);
                byte[] hashedBytes = hashAlgorithm.ComputeHash(bytes);
                string hashedLoginSecret = Convert.ToBase64String(hashedBytes);

                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestUri = "applogin.aspx?applicationid=" + Constants.ApplicaitonId.ToLower() + "&loginid=" + loginId + "&ticket=" + hashedLoginSecret + "&language=" + deviceLanguage;
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                var response = await httpClient.SendAsync(request);

                UserDialogs.Instance.HideLoading();

                var result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("auth result:" + result);

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    App.WebViewHTMLSource = result;
                    return true;
                }
                else
                {
                    await HandleError("Login Failed");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                await HandleError("Login Failed");
                return false;
            }
        }
        #endregion

        async Task HandleError(string error_message)
        {
            UserDialogs.Instance.HideLoading();
            await UserDialogs.Instance.AlertAsync(error_message, "", "OK");
        }
    }
}
