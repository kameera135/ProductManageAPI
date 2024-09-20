using ProductManageAPI.DTO;

namespace ProductManageAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> loginUser(UserDTO user);
        Task<string> createToken(UserDTO user);
    }
}
