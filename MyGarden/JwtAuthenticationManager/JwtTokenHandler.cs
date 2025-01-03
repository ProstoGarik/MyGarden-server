using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "55d96c54ca25a43252a8ab8ebf165697361d21355cc9ede33fd6c267f77508e599c6b955110d2a6bf5a3f2c5d88fea2d88341259f89d6942ee0648b8f71590b5eeccf12847ac918ce32985898a6b567dda1550c6a49e4a9ab24137590f61acafbc793f2a3c1a190e7a1f23d9c8714e09830f4f61d4e951f105dbfcf994499183e01cef1a942aa57e0aced206c6093ee4a5cc7821756121b4b06e35174d340f679582881a176d813814a139e7d8bed795f55e857ef12b404a4c4474eb1a406ca3ebbdb2585f9c3d30b714231f898525aab8293451d5cc3479ee92173b209c0adbf51a86e739cf2209bc253cbe4d483aae5dffe15aa28c6631d63cf8346d46b20b";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;

        private readonly List<UserAccount> _userAccounts;

        public JwtTokenHandler()
        {
            _userAccounts = new List<UserAccount>()
            {
                new UserAccount{UserName="admin",Password="admin123",Role="Administrator" },
                new UserAccount{UserName="user01",Password="user01",Role="User" }
            };
        }

        public AuthResponse? GenerateJwtToken(AuthRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password)) return null;

            var user = _userAccounts.Where(x => x.UserName == request.UserName && x.Password == request.Password).FirstOrDefault();
            if (user == null) return null;

            var tokenExpiryTimestamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claims = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,request.UserName),
                new Claim("Role",user.Role),
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = tokenExpiryTimestamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthResponse
            {
                UserName = user.UserName,
                ExpiresIn = (int)tokenExpiryTimestamp.Subtract(DateTime.Now).TotalSeconds,
                Role = user.Role,
                Token = token
            };

        }
    }
}
