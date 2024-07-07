namespace MyProgressTracker.Models
{
    public class AddSessionViewModel
    {
        public List<SubjectViewModel>?  subjects{ get; set; }
        public StudySessionViewModel Session { get; set; }
    }
}
