using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProgressTracker.Models.Entity
{
    public class StudySession
    {
        [Key]
        [Required]
        public int SessionId { get; set; }
        [Required]
        public string SessionName { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public string SubjectName { get; set; }
        public int? UserId { get; set; }
        [Required]
        public DateTime SessionStartTime { get; set; }
        [Required]
        public DateTime SessionEndTime { get; set; }
        public string? Description { get; set; } 
        
        // Default constructor
        public StudySession() { }

        // Parameterized constructor
        public StudySession(int sessionId, string sessionName, int subjectId, string subjectName, DateTime sessionStartTime, DateTime sessionEndTime, int? userId = null, string? description = null)
        {
            SessionId = sessionId;
            SessionName = sessionName;
            SubjectId = subjectId;
            SubjectName = subjectName;
            SessionStartTime = sessionStartTime;
            SessionEndTime = sessionEndTime;
            UserId = userId;
            Description = description;
        }


    }
}
