﻿using System.ComponentModel.DataAnnotations;

namespace MyProgressTracker.Models
{
    public class StudySubjectProgressReportModel
    {
        [Required]
        public string SubjectName { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public int NoOfSessions { get; set; }
        [Required]
        public TimeSpan TotalStudyDuration { get; set; }
        [Required]
        public TimeSpan TotalCourseDuration { get; set; }
        [Required]
        public double Score { get; set; }
    }
}
