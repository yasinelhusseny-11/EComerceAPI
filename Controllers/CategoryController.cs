using AutoMapper;
using EComerceAPI.DTOs;
using EComerceAPI.Models;
using EComerceAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EComerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryRepository.GetAll();
            var categoryDtos = _mapper.Map<IEnumerable<Category>>(categories);
            return Ok(categoryDtos);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }
        [HttpPost]
        public IActionResult Add(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            _categoryRepository.Add(category);
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);
        }
        [HttpPut]
        public IActionResult Update(int id, UpdateCategoryDto dto)
        {
            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }
                
            _mapper.Map(dto, category);

            _categoryRepository.Update(category);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.GetById(id);

            if (category == null)
                return NotFound();

            _categoryRepository.Delete(category);

            return Ok();
        }
    }
}
