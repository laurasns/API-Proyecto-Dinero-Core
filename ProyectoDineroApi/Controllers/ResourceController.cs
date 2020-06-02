using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using ProyectoDineroApi.Mappings;
using ProyectoDineroApi.Models;
using ProyectoDineroApi.Repositories;
using ProyectoDineroNuGet.Models;

namespace ProyectoDineroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        IRepository<Resource> repository;

        public ResourceController(IRepository<Resource> repository)
        {
            this.repository = repository;
        }

        [HttpGet("[action]")]
        public ActionResult<ApiResponse<List<ResourceDTO>>> GetResources()
        {
            List<Resource> resources = this.repository.GetAll();
            List<ResourceDTO> resesDTO = new List<ResourceDTO>();
            foreach(Resource res in resources)
            {
                ResourceDTO resDTO = ResourceMapper.MapToDTO(res);
                resesDTO.Add(resDTO);
            }
            return new ApiResponse<List<ResourceDTO>>
            {
                Data = resesDTO
            };
        }
    }
}