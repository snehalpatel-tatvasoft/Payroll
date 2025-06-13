using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.Helper;
using System.Text;

namespace PalladiumPayroll.JWT
{
    public static class AuthenticationServiceExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSettings? jwtSetting = AppSettingsConfig.GetSection<JwtSettings>(configuration, "JWT");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = jwtSetting?.Audience,
                ValidIssuer = jwtSetting?.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key)),
                ClockSkew = TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });
        }
    }
}
