namespace NZWalks.API.Models.DTO
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public List<string>? UserRole { get; set; }
    }
}
 