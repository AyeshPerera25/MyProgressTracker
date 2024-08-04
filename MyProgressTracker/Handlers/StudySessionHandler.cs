using MyProgressTracker.DataResources;
using MyProgressTracker.Models;
using MyProgressTracker.ServiceConnectors;
using MyProgressTrackerDependanciesLib.Models.DataTransferObjects;
using MyProgressTrackerDependanciesLib.Models.Entities;
using System.Linq;

namespace MyProgressTracker.Handlers
{
    public class StudySessionHandler
    {
		private InquiryServiceConnector _inquiryServiceConnector;

		public StudySessionHandler(InquiryServiceConnector inquiryServiceConnector)
		{
			_inquiryServiceConnector = inquiryServiceConnector;
		}

		internal StudySessionResponse? getAllSessions(string? sessionKey, long userID)
        {
            StudySessionResponse studySessionResponse = new StudySessionResponse();
			validateSessionData(sessionKey, userID);
            GetAllStudySessionsReq request = populateGetAllStudySessionReq(sessionKey, userID);
            GetAllStudySessionsRes getAllStudySessionsRes = _inquiryServiceConnector.GetUserAllStudySessionsAsync(request).GetAwaiter().GetResult();
            validateGetAllStudySessionRes(getAllStudySessionsRes);
			validateAllSessions(getAllStudySessionsRes.StudySessionsList); 
            populateStudySessionResponse(studySessionResponse, getAllStudySessionsRes.StudySessionsList);

			studySessionResponse.IsRequestSuccess = true;
            studySessionResponse.Description = "SessionLoading Successful!";
            return studySessionResponse;
        }

		private void validateGetAllStudySessionRes(GetAllStudySessionsRes response)
		{
			if (response != null)
			{
				if (!response.IsRequestSuccess)
				{
					throw new Exception("Get All Study Session Req has failed due to: " + response.Description);
				}
			}
			else
			{
				throw new Exception("Get All Study Session Res not found! ");
			}
		}

		private GetAllStudySessionsReq populateGetAllStudySessionReq(string? sessionKey, long userID)
        {
            GetAllStudySessionsReq request = new GetAllStudySessionsReq();
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

		private void populateStudySessionResponse(StudySessionResponse studySessionResponse, List<StudySession> sessions)
        {
            StudySessionViewModel studySessionViewModel;
            List<StudySessionViewModel> sessionList = new List<StudySessionViewModel>();
            Subject? subject;
            Course? course;
            if (sessions.Count >= 0)
            {
                foreach (StudySession session in sessions)
                {
                    studySessionViewModel = new StudySessionViewModel();
                    populateStudySessionViewModel(studySessionViewModel,session);
                    sessionList.Add(studySessionViewModel);
                }
            }
            studySessionResponse.allSessions = sessionList;
        }

        private void populateStudySessionViewModel(StudySessionViewModel studySessionViewModel, StudySession session)
        {
            studySessionViewModel.SessionId = session.StudySessionId;
            studySessionViewModel.SessionName = session.SessionName;
            studySessionViewModel.SessionDescription = session.Description;
            studySessionViewModel.SessionStartTime = session.SessionStartTime;
            studySessionViewModel.SessionEndTime = session.SessionEndTime;
            studySessionViewModel.SemestersNo = session.Subject.SemesterNo;
            studySessionViewModel.SubjectID = session.Subject.SubjectId;
            studySessionViewModel.SubjectName = session.Subject.SubjectName;
            studySessionViewModel.CourseID = session.Subject.Course.CourseId;
            studySessionViewModel.CourseName = session.Subject.Course.CourseName;
        }

        private void validateAllSessions(List<StudySession> sessions)
        {
            if (sessions == null)
            {
                throw new Exception("Sessions Loading Error!");
            }
            if (sessions.Count == 0)
            {
                throw new Exception("No Sessions Has Found!");
            }
        }

        internal StudySessionResponse addSession(AddSessionViewModel model, string? sessionKey, long userID)
        {
            StudySessionResponse response = new StudySessionResponse();
            StudySession session = new StudySession();
			validateSessionData(sessionKey, userID);
			validateStudySessionViewModel(model);
            AddStudySessionReq request = populateAddStudySessionReq(model, sessionKey, userID);
            AddStudySessionRes addStudySessionRes = _inquiryServiceConnector.AddNewStudySessionAsync(request).GetAwaiter().GetResult();
            validateAddStudySessionRes(addStudySessionRes);

			response.IsRequestSuccess = true;
			response.Description = "Add Subject Successful!";
			response.session = session;
            response.sessionModel = model.Session;
			return response;
		}

		private void validateAddStudySessionRes(AddStudySessionRes response)
		{
			if (response != null)
			{
				if (!response.IsRequestSuccess)
				{
					throw new Exception("Add Study Session Req has failed due to: " + response.Description);
				}
			}
			else
			{
				throw new Exception("Add Study Session Res not found! ");
			}
		}

		private AddStudySessionReq populateAddStudySessionReq(AddSessionViewModel model, string? sessionKey, long userID)
		{
			AddStudySessionReq request = new AddStudySessionReq();
			request.SessionName = model.Session.SessionName;
            request.SubjectID = model.Session.SubjectID;
            request.SessionDescription = model.Session.SessionDescription;
            request.SessionStartTime = model.Session.SessionStartTime;
            request.SessionEndTime = model.Session.SessionEndTime;
            request.SessionKey = sessionKey;
            request.UserId = userID;
            return request;
		}

		private void validateStudySessionViewModel(AddSessionViewModel model)
		{
			if (model == null || model.Session == null)
			{
				throw new Exception("New Study Session Request Model is Null! ");
			}
			if (model.Session.SessionName == null || model.Session.SessionName == string.Empty)
			{
				throw new Exception("New Study Session Name has not defined! ");
			}
			if (model.Session.SubjectID <= 0)
			{
				throw new Exception("New Study Session SubjectId Invalid!");
			}
			if (model.Session.SessionStartTime == null)
			{
				throw new Exception("New Study Session Start Time has not defined! ");
			}
			if (model.Session.SessionEndTime == null )
			{
				throw new Exception("New Study Session End Time has not defined! ");
			}
		}
    }
}
