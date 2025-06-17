using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace PalladiumPayroll.Helper.JWTToken
{
    public class JWTTokenService
    {
        private readonly IConfiguration _configuration;
        public JWTTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(IEnumerable<Claim> claims, DateTime expires, string key)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string AuthenticationToken(TokenDataModel tokenDataModel)
        {
            List<Claim>? claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, tokenDataModel.Email),
                new Claim(ClaimTypes.Name, tokenDataModel.UserName),
                new Claim(ClaimTypes.Role, tokenDataModel.RoleId.ToString()),
                new Claim("UserId", tokenDataModel.UserId.ToString()),
            };

            // Dictionary for role-based claims
            Dictionary<int, string>? roleClaims = new Dictionary<int, string>
            {
                { (int)DBEnums.Roles.Student, "StudentId" },
                { (int)DBEnums.Roles.Parent, "ParentId" },
                { (int)DBEnums.Roles.Teacher, "StaffId" },
                { (int)DBEnums.Roles.Librarian, "StaffId" },
                { (int)DBEnums.Roles.ShopKeeper, "StaffId" },
                { (int)DBEnums.Roles.ExternalSupervisor, "StaffId" },
                { (int)DBEnums.Roles.Driver, "DriverId" },
                { (int)DBEnums.Roles.Helper, "HelperId" },
            };

            // Add claim if role exists in the dictionary
            if (roleClaims.TryGetValue(tokenDataModel.roleId, out string? claimType))
            {
                claims.Add(new Claim(claimType, tokenDataModel.userTypeTableId.ToString()));
            }

            if (!string.IsNullOrEmpty(tokenDataModel.guid))
            {
                claims.Add(new Claim("TokenGuid", tokenDataModel.guid));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]!,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]!)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityTokenHandler().CreateToken(tokenDescriptor));
        }

        //public string RefreshToken(TokenDataModel tokenDataModel)
        //{
        //    return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityTokenHandler().CreateToken(new SecurityTokenDescriptor
        //    {
        //        Subject = new System.Security.Claims.ClaimsIdentity(
        //            new Claim[]
        //            {
        //            new Claim(ClaimTypes.Name, tokenDataModel.userName),
        //            }
        //        ),
        //        Issuer = _configuration["JWT:Issuer"],
        //        Audience = _configuration["JWT:Audience"]!,
        //        Expires = DateTime.UtcNow.AddDays(10),
        //        SigningCredentials = new(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]!)), SecurityAlgorithms.HmacSha256Signature)
        //    }));

        //}
    }
}
