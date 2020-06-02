using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoDineroApi.Helpers;
using ProyectoDineroApi.Mappings;
using ProyectoDineroApi.Models;
using ProyectoDineroApi.Repositories;
using ProyectoDineroNuGet.Models;

namespace ProyectoDineroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [Authorize]
        [HttpGet("[action]")]
        public ActionResult<ApiResponse<UserDTO>> GetUser()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            String json = claims.SingleOrDefault(x => x.Type == "UserData").Value;
            User user = JsonConvert.DeserializeObject<User>(json);
            UserDTO userDto = UserMapper.MapToDTO(user);
            return new ApiResponse<UserDTO>
            {
                Data = userDto
            };
        }

        [Authorize]
        [HttpGet("[action]/{username}")]
        public ActionResult<ApiResponse<UserDTO>> GetUserByUsername(String username)
        {
            User user = this.repository.GetUserByUsername(username);
            
            UserDTO userDto = UserMapper.MapToDTO(user);
            return new ApiResponse<UserDTO>
            {
                Data = userDto
            };
        }

        [Authorize]
        [HttpGet("[action]/{id}")]
        public ActionResult<ApiResponse<UserDTO>> GetUserById(int id)
        {
            User user = this.repository.Get(id);
            UserDTO userDto = UserMapper.MapToDTO(user);
            return new ApiResponse<UserDTO>
            {
                Data = userDto
            };
        }

        [Authorize]
        [HttpGet("[action]/{userId}")]
        public ActionResult<int> GetUserRole(int userId)
        {
            return this.repository.GetUserRole(userId);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<ApiResponse<UserDTO>> AddUser(UserDTO userDto)
        {
            if (userDto == null)
            {
                return new ApiResponse<UserDTO> { ErrorMessage = "Error al crear el usuario" };
            }

            bool userExist = this.repository.GetFiltered(u => u.Username == userDto.Username).Any() || this.repository.GetFiltered(u => u.Email == userDto.Email).Any();

            if (userExist)
            {
                return new ApiResponse<UserDTO> { ErrorMessage = "El nombre de usuario o el email ya existen." };
            }
            string passwordSalt = SecurityHelper.GenerateSalt();
            User user = UserMapper.MapToModel(userDto);
            user.Password = SecurityHelper.CreateHash(user.Password, passwordSalt);
            user.PasswordSalt = passwordSalt;

            User createdUser = this.repository.Add(user);

            return new ApiResponse<UserDTO>
            {
                Data = UserMapper.MapToDTO(createdUser)
            };
        }

        [Authorize]
        [HttpPut]
        [Route("[action]")]
        public void EditUser(UserDTO userDto)
        {
            User user = UserMapper.MapToModel(userDto);
            this.repository.Edit(user);
        }

        [Authorize]
        [HttpDelete]
        [Route("[action]/{UserId}")]
        public void DeleteUser(int UserId)
        {
            User user = this.repository.Get(UserId);
            this.repository.Delete(user);            
        }

    }
}