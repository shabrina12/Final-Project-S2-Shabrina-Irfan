using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppDTS_API.ViewModels
{
    public class LoginVM
    {
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //[Column("refresh_token")]
        //public string? RefreshToken { get; set; }
        //[Column("refresh_token_expiry_time")]
        //public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
