using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using logTesting.Dtos;
using logTesting.Models;
using logTesting.Repo;
using Microsoft.AspNetCore.Mvc;

namespace logTesting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryRepo categoryRepo, IMapper mapper, ILogger<CategoriesController> logger)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAllCategories()
        {
            _logger.LogInformation("Getting all categories");
            var categories =  await _categoryRepo.GetCategories();
            Console.WriteLine(categories);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_mapper.Map<IEnumerable<CategoryReadDto>>(categories));
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<ActionResult<CategoryReadDto>> GetCategoryById(int id)
        {
            _logger.LogInformation($"Getting category with id: {id}");
            var category = await _categoryRepo.GetCategory(id);
            if (category != null)
            {
                return Ok(_mapper.Map<CategoryReadDto>(category));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<CategoryReadDto> CreateCategory([FromBody]CategoryCreateDto categoryCreateDto)
        {
            _logger.LogInformation($"Creating category with name: {categoryCreateDto.Name}");
            if(categoryCreateDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_categoryRepo.CategoryExists(categoryCreateDto.Name))
            {
                ModelState.AddModelError("", "Category already exists!");
                return StatusCode(404, ModelState);
            }
            var categoryModel = _mapper.Map<Category>(categoryCreateDto);
            if (!_categoryRepo.AddCategory(categoryModel))
            {
                ModelState.AddModelError("", $"Something went wrong saving the category {categoryModel.Name}");
                return StatusCode(500, ModelState);
            }

            var categoryReadDto = _mapper.Map<CategoryReadDto>(categoryModel);

            return CreatedAtRoute(nameof(GetCategoryById), new { Id = categoryReadDto.Id }, categoryReadDto);
        }
    }
}