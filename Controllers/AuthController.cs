using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManageAPI.Interfaces;
using ProductManageAPI.Models;

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
    }
}
