namespace YouthVoice.Data
{
    public class Member
    {
        public int MemberId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; }
    }
}
