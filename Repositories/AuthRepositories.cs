using Microsoft.IdentityModel.Tokens;
using ProductManageAPI.DTO;
using ProductManageAPI.Interfaces;
using ProductManageAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProductManageAPI.Repositories
{
    public class AuthRepositories : IAuthRepository
    {
        private readonly PmsContext m_umsContext;
        private readonly IConfiguration m_configuration;

        public AuthRepositories(PmsContext umsContext, IConfiguration configuration)
        {
            m_umsContext = umsContext;
            m_configuration = configuration;
        }

        public async Task<string> createToken(UserDTO user)
        {
            try
            {
                List<Claim> claims = new List<Claim> { };

                User? userDetails = m_umsContext.Users.Where(u=> u.UserName == user.UserName).FirstOrDefault();

                if (userDetails == null)
                {
                    throw new Exception("User not found");
                }

                claims.Add(new Claim("id", userDetails.UserId.ToString()));
                claims.Add(new Claim("username", userDetails.UserName.ToString()));
                claims.Add(new Claim("fName", userDetails.FirstName.ToString()));
                claims.Add(new Claim("lName", userDetails.LastName.ToString()));
                claims.Add(new Claim("email", userDetails.Email.ToString()));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: m_configuration.GetSection("Jwt:Key").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(7), //make this more limit time like minitue
                    signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
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
                User? tempUser = m_umsContext.Users.Where(u=> u.UserName == user.UserName && u.DeletedAt == null).FirstOrDefault();

                if (tempUser == null && tempUser.UserName == null)
                {
                    return false;
                }

                string incomingPasswordHash = getPasswordHash(user.Password, tempUser.PasswordSalt);

                if(incomingPasswordHash != tempUser.PasswordHash) 
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string getPasswordHash(string password, string salt)
        {
            // Combine the password and salt
            string combinedPassword = MD5(salt) + password;

            // Choose the hash algorithm (SHA-256 or SHA-512)
            using (var sha512 = SHA512.Create())
            {
                // Convert the combined password string to a byte array
                byte[] bytes = Encoding.UTF8.GetBytes(combinedPassword);

                // Compute the hash value of the byte array
                byte[] hash = sha512.ComputeHash(bytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("x2"));
                }

                return result.ToString();
            }
        }

        protected string MD5(string text)
        {
            using var provider = System.Security.Cryptography.MD5.Create();
            StringBuilder builder = new StringBuilder();

            foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(text)))
                builder.Append(b.ToString("x2").ToLower());

            return builder.ToString();
        }
    }
}
