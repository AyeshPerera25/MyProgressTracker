﻿using MyProgressTracker.DataResources;
using MyProgressTracker.Models;
using MyProgressTracker.ServiceConnectors;
using MyProgressTrackerDependanciesLib.Models.DataTransferObjects;
using MyProgressTrackerDependanciesLib.Models.Entities;

namespace MyProgressTracker.Handlers
{
    public class CourseHandler
    {
        
        private InquiryServiceConnector _inquiryServiceConnector;

		public CourseHandler(InquiryServiceConnector inquiryServiceConnector)
		{
			_inquiryServiceConnector = inquiryServiceConnector;
		}

		public CourseResponse getAllCourses(string? sessionKey, long userID)
        {
            CourseResponse response = new CourseResponse();
            validateSessionData(sessionKey, userID);
            GetAllCoursesReq request = populateGetAllCourseReq(sessionKey, userID);
			GetAllCoursesRes getAllCoursesRes = _inquiryServiceConnector.GetUserAllCoursesAsync(request).GetAwaiter().GetResult();
            validateGetAllCoursesRes(getAllCoursesRes);
			populateCorseResponse(response, getAllCoursesRes.CourseList);

			response.IsRequestSuccess = true;
			response.Description = "Course Loading Successful!";
            return response;
        }

		private void populateCorseResponse(CourseResponse courseResponse, List<Course> courses)
		{
			CoursesViewModel coursesViewModel;
			List<CoursesViewModel> courseList = new List<CoursesViewModel>();
			if (courses.Count >= 0)
			{
				foreach (Course course in courses)
				{
					coursesViewModel = new CoursesViewModel();
					populateCourseViewModel(coursesViewModel, course);
					/*populateSemesterCount(coursesViewModel);*/
					courseList.Add(coursesViewModel);
				}
			}
			courseResponse.allCourses = courseList;
		}

		private void validateGetAllCoursesRes(GetAllCoursesRes response)
		{
			if (response != null)
			{
				if (!response.IsRequestSuccess)
				{
					throw new Exception("Get All Courses Req has failed due to: " + response.Description);
				}
			}
			else
			{
				throw new Exception("Get All Courses Req not found! ");
			}
		}

		private GetAllCoursesReq populateGetAllCourseReq(string? sessionKey, long userID)
		{
			GetAllCoursesReq request = new GetAllCoursesReq();
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
            if(userID <= 0L)
            {
				throw new Exception("Invalid User ID! "+ string.Concat(userID));
			}
		}

        private void populateCourseViewModel(CoursesViewModel coursesViewModel, Course course)
        {
            coursesViewModel.CourseId = course.CourseId;
            coursesViewModel.CourseName = course.CourseName;
            coursesViewModel.CourseStartDate = course.CourseStartDate;
            coursesViewModel.CourseEndDate = course.CourseEndDate;
            coursesViewModel.CourseDescription = course.CourseDescription;
            coursesViewModel.UniversityName = course.UniversityName;
            coursesViewModel.NoOfSemesters = course.NoOfSemesters;
        }

        internal CourseResponse? addCourse(CoursesViewModel model, string? sessionKey, long userID)
        {
            CourseResponse courseResponse = new CourseResponse();
            Course course = new Course();
            AddCourseReq request = null;
			validateSessionData(sessionKey, userID);
			validateNewCourseReqModel(model);
            request = populateAddCourseReq(model, sessionKey, userID);
            AddCourseRes addCourseRes = _inquiryServiceConnector.AddNewCoursesAsync(request).GetAwaiter().GetResult();
            validateAddCourseRes(addCourseRes);
	
            courseResponse.IsRequestSuccess = true;
            courseResponse.Description = "Add Course Successful!";
            courseResponse.course = addCourseRes.course;
            courseResponse.courseModel = model;
            return courseResponse;
        }

		private void validateAddCourseRes(AddCourseRes response)
		{
			if (response != null)
			{
				if (!response.IsRequestSuccess)
				{
					throw new Exception("Add Courses Req has failed due to: " + response.Description);
				}
			}
			else
			{
				throw new Exception("Add Courses Res not found! ");
			}
		}

		private AddCourseReq? populateAddCourseReq(CoursesViewModel model, string? sessionKey, long userID)
		{
			AddCourseReq request = new AddCourseReq();
            request.CourseName = model.CourseName;
            request.CourseDescription = model.CourseDescription;
            request.CourseStartDate = model.CourseStartDate;
            request.CourseEndDate = model.CourseEndDate;
            request.NoOfSemesters = model.NoOfSemesters;
            request.UniversityName = model.UniversityName;
            request.UserId = userID;
            request.SessionKey = sessionKey;
            return request;
		}

		private void populateNewCourse(Course course, CoursesViewModel model)
        {
            course.CourseName = model.CourseName;
            course.CourseStartDate = model.CourseStartDate;
            course.CourseEndDate = model.CourseEndDate;
            course.CourseDescription = model.CourseDescription;
            course.UniversityName = model.UniversityName;
            course.NoOfSemesters = model.NoOfSemesters;
        }

        private void validateNewCourseReqModel(CoursesViewModel model)
        {
            if (model == null)
            {
                throw new Exception("New Course Request Model is Null! " );
            }
            if(model.CourseName == null || model.CourseName == "")
            {
                throw new Exception("New Course Name has not defined! ");
            }
            if (model.UniversityName == null || model.UniversityName == "")
            {
                throw new Exception("New Course Institute Name has not defined! ");
            }
        }

        internal CourseResponse deleteCourse(int courseID, string? sessionKey, long userID)
        {
            CourseResponse response = new CourseResponse();
            validateSessionData(sessionKey, userID);
            DeleteCoursReq request = populateDeleteCourseReq(courseID,sessionKey, userID);
            CommenResponse comResponse = _inquiryServiceConnector.DeleteCourseAsync(request).GetAwaiter().GetResult();
            validateDeleteCommenResponse(comResponse);
            
            response.IsRequestSuccess = true;
            response.Description = "Course Delete Successful!";
            return response;
        }

        private void validateDeleteCommenResponse(CommenResponse response)
        {
            if (response != null)
            {
                if (!response.IsRequestSuccess)
                {
                    throw new Exception("Delete Req has failed due to: " + response.Description);
                }
            }
            else
            {
                throw new Exception("Delete Res not found! ");
            }
        }

        private DeleteCoursReq populateDeleteCourseReq(int courseID, string? sessionKey, long userID)
        {
            DeleteCoursReq request = new DeleteCoursReq();
            request.CourseId = courseID;
            request.SessionKey = sessionKey;    
            request.UserId = userID;
            return request;
        }

        public CourseResponse deleteSubject(int subjectId, string? sessionKey, long userID)
        {
            CourseResponse response = new CourseResponse();
            validateSessionData(sessionKey, userID);
            DeleteSubjectReq request = populateDeleteSubjectReq(subjectId, sessionKey, userID);
            CommenResponse comResponse = _inquiryServiceConnector.DeleteSubjectAsync(request).GetAwaiter().GetResult();
            validateDeleteCommenResponse(comResponse);

            response.IsRequestSuccess = true;
            response.Description = "Subject Delete Successful!";
            return response;
        }

        private DeleteSubjectReq populateDeleteSubjectReq(int subjectId, string? sessionKey, long userID)
        {
            DeleteSubjectReq request = new DeleteSubjectReq();
            request.SubjectId = subjectId;
            request.SessionKey = sessionKey;
            request.UserId = userID;
            return request;
        }

        public CourseResponse deleteStudySession(int sessionId, string? sessionKey, long userID)
        {
            CourseResponse response = new CourseResponse();
            validateSessionData(sessionKey, userID);
            DeleteStudySessionReq request = populateDeleteStudySessionReq(sessionId, sessionKey, userID);
            CommenResponse comResponse = _inquiryServiceConnector.DeleteStudySessionAsync(request).GetAwaiter().GetResult();
            validateDeleteCommenResponse(comResponse);

            response.IsRequestSuccess = true;
            response.Description = "Study Session Delete Successful!";
            return response;
        }

        private DeleteStudySessionReq populateDeleteStudySessionReq(int sessionId, string? sessionKey, long userID)
        {
            DeleteStudySessionReq request = new DeleteStudySessionReq();
            request.StudySessionId = sessionId;
            request.SessionKey = sessionKey;
            request.UserId = userID;
            return request; 
        }
    }
}
