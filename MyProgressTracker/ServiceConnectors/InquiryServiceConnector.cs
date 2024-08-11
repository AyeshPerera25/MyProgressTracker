using MyProgressTrackerDependanciesLib.Models.DataTransferObjects;
using Newtonsoft.Json;

namespace MyProgressTracker.ServiceConnectors
{
	public class InquiryServiceConnector
	{
		private readonly HttpClient _httpClient;
		//private string _baseUrl = "https://myprogresstrackerapigateway.azure-api.net/inquiry";
		private string _baseUrl = "http://localhost:5011";

		public InquiryServiceConnector()
		{
			_httpClient = new HttpClient();
			//_httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ddd78940f70d4313851de1564789dd60");
		}
		public async Task<ProgressReportRes> GetUserProgressReportAsync(ProgressReportReq request)
		{
			string jsonPayload = JsonConvert.SerializeObject(request);
			Console.WriteLine(jsonPayload); // Print the JSON payload
			var response = await _httpClient.PostAsJsonAsync(_baseUrl+"/api/DashboardInquiry/getProgressReport", request);
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsAsync<ProgressReportRes>();
			}
			else
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {response.StatusCode}, {errorContent}");
			}
			return null;
		}
		public async Task<GetAllCoursesRes> GetUserAllCoursesAsync(GetAllCoursesReq request)
		{
			string jsonPayload = JsonConvert.SerializeObject(request);
			Console.WriteLine(jsonPayload); // Print the JSON payload
			var response = await _httpClient.PostAsJsonAsync(_baseUrl + "/api/DashboardInquiry/getAllCourses", request);
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
			var response = await _httpClient.PostAsJsonAsync(_baseUrl + "/api/DashboardInquiry/addNewCourse", request);
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

		public async Task<GetAllSubjectsRes> GetUserAllSubjectAsync(GetAllSubjectsReq request)
		{
			string jsonPayload = JsonConvert.SerializeObject(request);
			Console.WriteLine(jsonPayload); // Print the JSON payload
			var response = await _httpClient.PostAsJsonAsync(_baseUrl + "/api/DashboardInquiry/getAllSubjects", request);
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsAsync<GetAllSubjectsRes>();
			}
			else
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {response.StatusCode}, {errorContent}");
			}
			return null;
		}

		public async Task<AddNewSubjectRes> AddNewSubjectAsync(AddNewSubjectReq request)
		{
			string jsonPayload = JsonConvert.SerializeObject(request);
			Console.WriteLine(jsonPayload); // Print the JSON payload
			var response = await _httpClient.PostAsJsonAsync(_baseUrl + "/api/DashboardInquiry/addNewSubject", request);
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsAsync<AddNewSubjectRes>();
			}
			else
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {response.StatusCode}, {errorContent}");
			}
			return null;
		}

		public async Task<GetAllStudySessionsRes> GetUserAllStudySessionsAsync(GetAllStudySessionsReq request)
		{
			string jsonPayload = JsonConvert.SerializeObject(request);
			Console.WriteLine(jsonPayload); // Print the JSON payload
			var response = await _httpClient.PostAsJsonAsync(_baseUrl + "/api/DashboardInquiry/getAllStudySessions", request);
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsAsync<GetAllStudySessionsRes>();
			}
			else
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {response.StatusCode}, {errorContent}");
			}
			return null;
		}

		public async Task<AddStudySessionRes> AddNewStudySessionAsync(AddStudySessionReq request)
		{
			string jsonPayload = JsonConvert.SerializeObject(request);
			Console.WriteLine(jsonPayload); // Print the JSON payload
			var response = await _httpClient.PostAsJsonAsync(_baseUrl + "/api/DashboardInquiry/addNewStudySession", request);
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsAsync<AddStudySessionRes>();
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
