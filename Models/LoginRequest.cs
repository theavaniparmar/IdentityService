using System.Runtime.CompilerServices;

namespace IdentityService.Models
{
    public class LoginRequest
    { 
        
        public string Username {  get; set; }
        
        public string Password { get; set; }
    }
}
