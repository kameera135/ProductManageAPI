using ProductManageAPI.DTO;
using ProductManageAPI.Interfaces;

namespace ProductManageAPI.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IAuthRepository m_authRepository;

        public AuthServices(IAuthRepository authRepository)
        {
            m_authRepository = authRepository;
        }

        public async Task<string> createToken(UserDTO user)
        {
            try
            {
                var tempToken = await m_authRepository.createToken(user);
                return tempToken.ToString();

            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> loginUser(UserDTO user)
        {
            try
            {
                var tempUser = await m_authRepository.loginUser(user);
                return tempUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
