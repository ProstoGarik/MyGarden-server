using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthContoller : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;

        public AuthContoller(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }
        [HttpPost]
        public ActionResult<AuthResponse?> Auth([FromBody] AuthRequest request)
        {
            var authResponse = _jwtTokenHandler.GenerateJwtToken(request);
            if (authResponse == null) return Unauthorized();
            return Ok(authResponse);
        }
    }

}
