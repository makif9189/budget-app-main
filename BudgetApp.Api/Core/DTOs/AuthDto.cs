using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Api.Core.DTOs
{
    /// <summary>
    /// DTO for user registration requests.
    /// </summary>
    public class RegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }

    /// <summary>
    /// DTO for user login requests.
    /// </summary>
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// DTO to return upon successful authentication.
    /// </summary>
    public class AuthResponseDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
