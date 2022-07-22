namespace MicrosoftOutlook.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MicrosoftOutlook.Interface;
    using RestSharp;


    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IAuthClass AuthClass;

        public AuthController( IAuthClass authClass)
        {
            AuthClass = authClass;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return Redirect(AuthClass.Login());
        }

        [HttpGet("Token")]
        public IActionResult Tokent(string code, string state)
        {
            var result = AuthClass.Token(code, state);           
            return Redirect("https://localhost:7071/swagger/index.html");         
        }

        [HttpGet("RefreshToken")]
        public void RefreshToken()
        {
            var result = AuthClass.RefreshToken();
        }
    }
}
