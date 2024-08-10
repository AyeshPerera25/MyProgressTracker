using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyProgressTrackerDependanciesLib.Models.DataTransferObjects;
using Newtonsoft.Json;

namespace MyProgressTracker.ServiceConnectors
{
    public class AthenticationServiceConnector 
    {
        private readonly HttpClient _httpClient;
		private string _baseUrl = "https://myprogresstrackerapigateway.azure-api.net/auth";


		public AthenticationServiceConnector()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ddd78940f70d4313851de1564789dd60");
        }

        public async Task<NewUserRegistrationRes> UserRegisterAsync(NewUserRegistrationReq request)
        {
			string jsonPayload = JsonConvert.SerializeObject(request);
			Console.WriteLine(jsonPayload); // Print the JSON payload
			var response = await _httpClient.PostAsJsonAsync(_baseUrl+"/api/UserAuthentication/userRegister", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<NewUserRegistrationRes>();
            }
			else
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {response.StatusCode}, {errorContent}");
			}
			return null;
        }

        public async Task<UserLoginRes> UserLoginAsync(UserLoginReq request)
        {
			string jsonPayload = JsonConvert.SerializeObject(request);
			Console.WriteLine(jsonPayload); // Print the JSON payload
			var response = await _httpClient.PostAsJsonAsync(_baseUrl + "/api/UserAuthentication/userLogin", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<UserLoginRes>();
            }
			else
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {response.StatusCode}, {errorContent}");
			}
			return null;
        }
    }

}

