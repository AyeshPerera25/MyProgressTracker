using MyProgressTracker.DataResources;
using MyProgressTracker.Models;
using MyProgressTracker.ServiceConnectors;
using MyProgressTrackerDependanciesLib.Models.DataTransferObjects;
using MyProgressTrackerDependanciesLib.Models.Entities;

namespace MyProgressTracker.Handlers
{
    public class ReportHandler
    {
		private InquiryServiceConnector _inquiryServiceConnector;

		public ReportHandler(InquiryServiceConnector inquiryServiceConnector)
		{
			_inquiryServiceConnector = inquiryServiceConnector;
		}

		public List<StudySubjectProgressReportModel> getProgressReport(string? sessionKey, long userID)
        {
            StudySubjectProgressReportModel model;
            List<StudySubjectProgressReportModel> modelList = new List<StudySubjectProgressReportModel>();
			validateSessionData(sessionKey, userID);
			ProgressReportReq request = populateProgressReportReq(sessionKey, userID);
			ProgressReportRes progressReportRes = _inquiryServiceConnector.GetUserProgressReportAsync(request).GetAwaiter().GetResult();
			validateProgressReportRes(progressReportRes);
			populateStudySubjectProgressReportModelList(modelList, progressReportRes);

			return modelList;
		}

		private void populateStudySubjectProgressReportModelList(List<StudySubjectProgressReportModel> modelList, ProgressReportRes progressReportRes)
		{
			StudySubjectProgressReportModel model;
			foreach (ProgressReport progressReport in progressReportRes.ProgressReports)
			{
				model = new StudySubjectProgressReportModel();
				model.SubjectName = progressReport.SubjectName;
				model.CourseName = progressReport.CourseName;
				model.NoOfSessions = progressReport.NoOfSessions;
				model.TotalStudyDuration = progressReport.TotalStudyDuration;
				model.TotalCourseDuration = progressReport.TotalCourseDuration;
				model.Score = progressReport.Score;
				modelList.Add(model);
			}
		}

		private void validateProgressReportRes(ProgressReportRes response)
		{
			if (response != null)
			{
				if (!response.IsRequestSuccess)
				{
					throw new Exception("Get All Progress Report Req has failed due to: " + response.Description);
				}
			}
			else
			{
				throw new Exception("Get All Progress Report Res not found! ");
			}
			if (response.ProgressReports == null)
			{
				throw new Exception("Progress Report Loading Error!");
			}
			if (response.ProgressReports.Count == 0)
			{
				throw new Exception("No Progress Report Has Found!");
			}
		}

		private ProgressReportReq populateProgressReportReq(string? sessionKey, long userID)
		{
			ProgressReportReq request = new ProgressReportReq();
			request.SessionKey = sessionKey;
			request.UserId = userID;
			return request;
		}

		private void validateSessionData(string? sessionKey, long userID)
		{
			if (sessionKey == null)
			{
				throw new Exception("Session Key Not Found!");
			}
			if (sessionKey == string.Empty)
			{
				throw new Exception("Session Key is Empty!");
			}
			if (userID <= 0L)
			{
				throw new Exception("Invalid User ID! " + string.Concat(userID));
			}
		}
	}
}
