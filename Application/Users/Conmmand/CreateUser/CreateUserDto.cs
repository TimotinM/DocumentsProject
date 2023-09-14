namespace Application.Users.Conmmand.CreateUser
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public List<string> UserRoles { get; set; } = new List<string>();
        public int? IdInstitution { get; set; }
        public bool IsEnabled { get; set; }
    }
}
