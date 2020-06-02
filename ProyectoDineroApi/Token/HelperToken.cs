using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDineroApi.Token
{
    public class HelperToken
    {
        public String issuer { get; set; }
        public String audience { get; set; }
        public String secretkey { get; set; }

        public HelperToken(IConfiguration configuration)
        {
            this.issuer = configuration["MoneyApiAuth:Issuer"];
            this.audience = configuration["MoneyApiAuth:Audience"];
            this.secretkey = configuration["MoneyApiAuth:SecretKey"];
        }

        public SymmetricSecurityKey GetKeyToken()
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(this.secretkey);
            return new SymmetricSecurityKey(data);
        }

        public Action<JwtBearerOptions> GetJwtOptions()
        {
            Action<JwtBearerOptions> jwtoptions = new Action<JwtBearerOptions>(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = true, ValidateAudience = true, ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = this.issuer, ValidAudience = this.audience, IssuerSigningKey = this.GetKeyToken()
                };
            });
            return jwtoptions;
        }

        public Action<AuthenticationOptions> GetAuthOptions()
        {
            Action<AuthenticationOptions> authoptions = new Action<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            return authoptions;
        }
    }
}
