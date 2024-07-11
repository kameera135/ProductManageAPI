using ProductManageAPI.DTO;

namespace ProductManageAPI.Interfaces
{
    public interface IAuthRepository
    {
        bool loginUser(UserDTO user);
        string createToken(UserDTO user);
    }
}
