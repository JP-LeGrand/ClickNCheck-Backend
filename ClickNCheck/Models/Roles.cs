namespace ClickNCheck.Models
{
    public class Roles
    {
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}