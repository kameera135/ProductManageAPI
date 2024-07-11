using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManageAPI.DTO;
using ProductManageAPI.Interfaces;
using ProductManageAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ProductManageAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService m_authService;
        private readonly PmsContext m_pmsContext;

        public AuthController(IAuthService authService, PmsContext pmsContext)
        {
            m_authService = authService;
            m_pmsContext = pmsContext;
        }

        [HttpPost("user/login")]
        public IActionResult LoginUser(UserDTO user)
        {
            try
            {
                var userName = m_pmsContext.Users.Where(u=> u.UserName == user.UserName).FirstOrDefault();

                if(userName == null)
                {
                    return BadRequest("Wrong username.Kindly check it");
                }

                var result = m_authService.loginUser(user);
                var handler = new JwtSecurityTokenHandler();

                if (!result)
                {
                    return BadRequest("Wrong userName or password. Kindly check");
                }


                string token = m_authService.createToken(user);

                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    message = "Login Successfully",
                    Token = token,
                    Expire = handler.ReadToken(token).ValidTo,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
