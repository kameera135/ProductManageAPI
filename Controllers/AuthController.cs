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
        public async Task<IActionResult> LoginUser(UserDTO user)
        {
            try
            {
                var userName = m_pmsContext.Users.Where(u=> u.UserName == user.UserName).FirstOrDefault();

                if(userName == null)
                {
                    var errorResponse = new
                    {
                        Status = "Fail",
                        Message = "Wrong Username",
                    };

                    return StatusCode(StatusCodes.Status400BadRequest,errorResponse);
                }

                var result = await m_authService.loginUser(user);
                var handler = new JwtSecurityTokenHandler();

                if (!result)
                {
                    var errorResponse = new
                    {
                        Status = "Fail",
                        Message = "Wrong Password",
                    };

                    return StatusCode(StatusCodes.Status400BadRequest, errorResponse);
                }


                string token = await m_authService.createToken(user);

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
                var errorResponse = new
                {
                    Message = "Internal Server Error",
                    ErrorDetails = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
