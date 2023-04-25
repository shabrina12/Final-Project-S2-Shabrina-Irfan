using System.ComponentModel.DataAnnotations;

namespace Web_API.DataModels
{
    public class LoginDM
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
