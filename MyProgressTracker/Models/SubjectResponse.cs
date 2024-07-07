using MyProgressTracker.Models.Entity;

namespace MyProgressTracker.Models
{
    public class SubjectResponse : ResponseWrapper
    {
        public List<SubjectViewModel>? allSubjects { get; set; }
        public SubjectViewModel? subjectModel { get; set; } = null;
        public Subject? subject { get; set; } = null;
    }
}
