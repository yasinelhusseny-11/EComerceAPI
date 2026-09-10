using AutoMapper;
using EComerceAPI.DTOs;
using EComerceAPI.Models;
using EComerceAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace EComerceAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository,
         IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productRepository.GetAll();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productDtos);

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            var productDto = _mapper.Map<ProductDto>(product); 

            return Ok(productDto);
        }
        [HttpPost]
        public IActionResult Add(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            _productRepository.Add(product);
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
        [HttpPut]
        public IActionResult Update(int id, UpdateProductDto dto)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }
            _mapper.Map(dto, product);
            _productRepository.Update(product);
            var productDto =_mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productRepository.Delete(product);
            return Ok();
        }

    }

}