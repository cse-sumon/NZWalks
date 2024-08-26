namespace NZWalks.API.Models.DTO
{
    public class UserDto
    {
        public string? Id { get; set; }

        public string? UserName { get; set; }

        public string? UserEmail { get; set; }

        public List<string>? Roles { get; set; }


    }
}
