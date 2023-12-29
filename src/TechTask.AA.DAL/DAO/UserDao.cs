namespace TechTask.AA.DAL.DAO
{
    public class UserDao
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public RoleDao Role { get; set; } = null!;
    }
}
