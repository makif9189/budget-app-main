namespace BudgetApp.Api.Core.DTOs
{
    /// <summary>
    /// DTO for exposing user information safely to the client.
    /// Excludes sensitive information like the password hash.
    /// </summary>
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
