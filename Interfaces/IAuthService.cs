using ProductManageAPI.DTO;

namespace ProductManageAPI.Interfaces
{
    public interface IAuthService
    {
        Task<bool> loginUser(UserDTO user);
        Task<string> createToken(UserDTO user);
    }
}
