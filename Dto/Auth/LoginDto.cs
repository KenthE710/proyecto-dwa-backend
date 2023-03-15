namespace App.Dto.Auth
{
    public class UserDto
    {
        public int id { get; set; }
        public string? username { get; set; }
    }

    public class LoginDto
    {
        public UserDto? user { get; set; }
        public string? token { get; set; }
    }
}
