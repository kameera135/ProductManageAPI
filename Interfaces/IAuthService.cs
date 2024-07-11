using ProductManageAPI.DTO;

namespace ProductManageAPI.Interfaces
{
    public interface IAuthService
    {
        bool loginUser(UserDTO user);
        string createToken(UserDTO user);
    }
}
