using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoDineroApi.Mappings;
using ProyectoDineroApi.Models;
using ProyectoDineroApi.Repositories;
using ProyectoDineroNuGet.Models;

namespace ProyectoDineroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IRepository<Product> repository;

        public ProductController(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("[action]/{type}")]
        public ActionResult<ApiResponse<List<ProductDTO>>> GetProducts(String type)
        {
            List<Product> products = this.repository.GetFiltered(p => p.Type == type).ToList();
            List<ProductDTO> productsDTO = new List<ProductDTO>();
            foreach (Product prod in products)
            {
                ProductDTO productDTO = ProductMapper.MapToDTO(prod);
                productsDTO.Add(productDTO);
            }

            ApiResponse<List<ProductDTO>> response = new ApiResponse<List<ProductDTO>>()
            {
                Data = productsDTO,
            };
            return response;
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<ProductDTO>> GetProduct(int id)
        {
            Product product = this.repository.Get(id);
            ProductDTO productDto = ProductMapper.MapToDTO(product);

            ApiResponse<ProductDTO> response = new ApiResponse<ProductDTO>()
            {
                Data = productDto,
            };
            return response;
        }
    }
}