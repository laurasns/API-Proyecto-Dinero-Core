using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProyectoDineroApi.Helpers;
using ProyectoDineroApi.Models;
using ProyectoDineroApi.Repositories;
using ProyectoDineroApi.Token;
using ProyectoDineroNuGet.Models;

namespace ProyectoDineroApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IUserRepository repo;
        BlobsRepository blob;
        HelperToken helper;

        public AuthController(IUserRepository repo, IConfiguration configuration, BlobsRepository blob)
        {
            this.repo = repo;
            this.helper = new HelperToken(configuration);
            this.blob = blob;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(LoginDTO userlogin)
        {
            User filteredUser = this.repo.GetFiltered(u => userlogin.Username == u.Username && u.Activated).FirstOrDefault()
                                  ?? repo.GetFiltered(u => userlogin.Username == u.Email).FirstOrDefault();

            bool passwordIsCorrect = filteredUser.Password == SecurityHelper.CreateHash(userlogin.Password, filteredUser.PasswordSalt);
            if (filteredUser != null && passwordIsCorrect == true)
            {
                Claim[] claims = new[]
                {
                    new Claim("UserData", JsonConvert.SerializeObject(filteredUser))
                };
                JwtSecurityToken token = new JwtSecurityToken
                    (
                        issuer: helper.issuer,
                        audience: helper.audience,
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new { Response = new JwtSecurityTokenHandler().WriteToken(token)});
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("[action]")]
        public String GetBlobToken()
        {
            return this.blob.GetToken();
        }
    }
}