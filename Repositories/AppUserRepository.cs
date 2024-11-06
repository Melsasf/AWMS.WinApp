using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPCAA.WinApp.Models;
using WPCAA.WinApp.Properties;

namespace WPCAA.WinApp.Repositories
{
    public class AppUserRepository
    {
        private readonly string ApiAddress = Settings.Default.ApiAddress;
        public async Task<AppUser> LoginAsync(AppUser appUserLogin)
        {
            var client = new RestClient(ApiAddress);
            var request = new RestRequest("api/account/login/", Method.Post);
            request.AddJsonBody(appUserLogin);
            request.RequestFormat = DataFormat.Json;
            var response = await client.ExecuteAsync(request);
            var appUser = JsonConvert.DeserializeObject<AppUser>(response.Content);
            return appUser;
        }

        public async Task<string> RegisterAsync(AppUser registerAppUser)
        {
            var client = new RestClient(ApiAddress);
            var request = new RestRequest("api/account/register/", Method.Post);
            request.AddJsonBody(registerAppUser);
            request.RequestFormat = DataFormat.Json;
            var response = await client.ExecuteAsync(request);
            var message = JsonConvert.DeserializeObject<string>(response.Content);
            return message;
        }
        public async Task<List<AppUser>> GetUsers()
        {
            var client = new RestClient(ApiAddress);
            var request = new RestRequest("api/User/", Method.Get);
            request.RequestFormat = DataFormat.Json;
            var response = await client.ExecuteAsync(request);
            var message = JsonConvert.DeserializeObject<List<AppUser>>(response.Content);
            return message;
        }

        //public IRestResponse ChangePassword(AppUser appUser)
        //{
        //    var client = new RestClient(ApiAddress);
        //    var request = new RestRequest("api/login/changepassword/", Method.PUT);
        //    request.RequestFormat = DataFormat.Json;
        //    request.AddJsonBody(appUser);
        //    var response = client.Execute(request);
        //    return response;
        //}
    }
}
