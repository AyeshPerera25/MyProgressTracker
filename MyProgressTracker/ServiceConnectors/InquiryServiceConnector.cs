using MyProgressTrackerAuthenticationService.Models.DataTransferObjects;
using MyProgressTrackerInquiryService.Models.DataTransferObjects;
using Newtonsoft.Json;

namespace MyProgressTracker.ServiceConnectors
{
	public class InquiryServiceConnector
	{
		private readonly HttpClient _httpClient;

		public InquiryServiceConnector()
		{
			_httpClient = new HttpClient();
		}
		public async Task<GetAllCoursesRes> GetUserAllCoursesAsync(GetAllCoursesReq request)
		{
			string jsonPayload = JsonConvert.SerializeObject(request);
			Console.WriteLine(jsonPayload); // Print the JSON payload
			var response = await _httpClient.PostAsJsonAsync("http://localhost:5011/api/DashboardInquiry/getAllCourses", request);
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsAsync<GetAllCoursesRes>();
			}
			else
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {response.StatusCode}, {errorContent}");
			}
			return null;
		}

		public async Task<AddCourseRes> AddNewCoursesAsync(AddCourseReq request)
		{
			string jsonPayload = JsonConvert.SerializeObject(request);
			Console.WriteLine(jsonPayload); // Print the JSON payload
			var response = await _httpClient.PostAsJsonAsync("http://localhost:5011/api/DashboardInquiry/addNewCourse", request);
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsAsync<AddCourseRes>();
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
