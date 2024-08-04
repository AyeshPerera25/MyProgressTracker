using MyProgressTracker.Models;
using MyProgressTracker.ServiceConnectors;
using MyProgressTrackerDependanciesLib.Models.DataTransferObjects;
using MyProgressTrackerDependanciesLib.Models.Entities;

namespace MyProgressTracker.Handlers
{
    public class SubjectHandler 
    {
		private InquiryServiceConnector _inquiryServiceConnector;

		public SubjectHandler(InquiryServiceConnector inquiryServiceConnector)
		{
			_inquiryServiceConnector = inquiryServiceConnector;
		}

		internal SubjectResponse getAllSubjects(string? sessionKey, long userID)
        {
            SubjectResponse response = new SubjectResponse();
			validateSessionData(sessionKey, userID);
            GetAllSubjectsReq request = populateGetAllSubjectReq(sessionKey, userID);
            GetAllSubjectsRes getAllSubjectsRes = _inquiryServiceConnector.GetUserAllSubjectAsync(request).GetAwaiter().GetResult();
            validateGetAllSubjectRes(getAllSubjectsRes);
			validateAllSubjects(getAllSubjectsRes.SubjectList);
			populateSubjectResponse(response, getAllSubjectsRes.SubjectList);

			response.IsRequestSuccess = true;
			response.Description = "Subjects Loading Successful!";
			return response;
        }

		private void validateGetAllSubjectRes(GetAllSubjectsRes response)
		{
			if (response != null)
			{
				if (!response.IsRequestSuccess)
				{
					throw new Exception("Get All Subjects Req has failed due to: " + response.Description);
				}
			}
			else
			{
				throw new Exception("Get All Subjects Res not found! ");
			}
		}

		private GetAllSubjectsReq populateGetAllSubjectReq(string? sessionKey, long userID)
		{
			GetAllSubjectsReq request = new GetAllSubjectsReq();
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

		private void populateSubjectResponse(SubjectResponse subjectResponse, List<Subject> subjects)
        {
            SubjectViewModel subjectViewModel;
            List<SubjectViewModel> subjectList = new List<SubjectViewModel>();
            if (subjects.Count >= 0)
            {
                foreach (Subject subject in subjects)
                {
                    subjectViewModel = new SubjectViewModel();
                    populateSubjectViewModel(subjectViewModel, subject);
                    subjectList.Add(subjectViewModel);
                }
            }
            subjectResponse.allSubjects = subjectList;
        }

        private void populateSubjectViewModel(SubjectViewModel subjectViewModel, Subject subject)
        {
            subjectViewModel.SubjectId = subject.SubjectId;
            subjectViewModel.SubjectName = subject.SubjectName;
            subjectViewModel.CourseID = subject.Course.CourseId;
            subjectViewModel.CourseName = subject.Course.CourseName;
            subjectViewModel.universityName = subject.Course.UniversityName;
            subjectViewModel.SemesterNo = subject.SemesterNo;
            subjectViewModel.SemesterStartDate = subject.SemesterStartDate;
            subjectViewModel.SemesterEndDate = subject.SemesterEndDate;
            subjectViewModel.Description = subject.SubjectDescription;
        }

        private void validateAllSubjects(List<Subject>? subjects)
        {
            if (subjects == null)
            {
                throw new Exception("Subject Loading Error!");
            }
            if (subjects.Count == 0)
            {
                throw new Exception("No Subject Has Found!");
            }
        }

        internal SubjectResponse? addSubject(AddSubjectViewModel model, string? sessionKey, long userID)
        {
            SubjectResponse response = new SubjectResponse();
            Subject subject = new Subject();
			validateSessionData(sessionKey, userID);
			validateNewSubjectReqModel(model);
            AddNewSubjectReq request = populateAddNewSubjectReq(model, sessionKey, userID);
            AddNewSubjectRes addNewSubjectRes = _inquiryServiceConnector.AddNewSubjectAsync(request).GetAwaiter().GetResult();
            validateAddNewSubjectRes(addNewSubjectRes);

			response.IsRequestSuccess = true;
			response.Description = "Add Subject Successful!";
			response.subject = addNewSubjectRes.subject;
			response.subjectModel = model.Subject;
            return response;
        }

		private void validateAddNewSubjectRes(AddNewSubjectRes response)
		{
			if (response != null)
			{
				if (!response.IsRequestSuccess)
				{
					throw new Exception("Add Subject Req has failed due to: " + response.Description);
				}
			}
			else
			{
				throw new Exception("Add Subject Res not found! ");
			}
		}

		private AddNewSubjectReq populateAddNewSubjectReq(AddSubjectViewModel model, string? sessionKey, long userID)
		{
			AddNewSubjectReq request = new AddNewSubjectReq();
            request.SubjectName = model.Subject.SubjectName;
            request.CourseID = model.Subject.CourseID;
            request.Description = model.Subject.Description;
            request.SemesterNo = model.Subject.SemesterNo;
            request.SemesterStartDate = model.Subject.SemesterStartDate;
            request.SemesterEndDate = model.Subject.SemesterEndDate;
            request.SessionKey = sessionKey;
            request.UserId = userID;
            return request; 
		}

        private void validateNewSubjectReqModel(AddSubjectViewModel model)
        {
            if (model.Subject == null)
            {
                throw new Exception("New Subject Request Model is Null! ");
            }
            if (model.Subject.SubjectName == null || model.Subject.SubjectName == "")
            {
                throw new Exception("New Subject Name has not defined! ");
            }
            if (model.Subject.CourseID == 0)
            {
                throw new Exception("New Subject CourseId not found!");
            }
        }
    }
}
