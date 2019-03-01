namespace ClickNCheck.Models
{
    public class Recruiter_Candidate
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int RecruiterId { get; set; }
        public User Recruiter { get; set; }
    }
}