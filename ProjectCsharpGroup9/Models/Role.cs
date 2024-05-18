namespace ProjectCsharpGroup9.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get ; set; }
        public virtual List<User> Users { get; set; }
    }
}
