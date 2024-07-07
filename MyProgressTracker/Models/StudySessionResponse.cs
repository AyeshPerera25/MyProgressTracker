using MyProgressTracker.Models.Entity;

namespace MyProgressTracker.Models
{
    public class StudySessionResponse : ResponseWrapper
    {
        public List<StudySessionViewModel>? allSessions { get; set; }
        public StudySessionViewModel? sessionModel { get; set; } = null;
        public StudySession? session { get; set; } = null;
    }
}
