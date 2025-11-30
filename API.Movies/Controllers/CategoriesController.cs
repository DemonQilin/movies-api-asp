using API.Movies.DAL.Models.Dtos;
using API.Movies.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>A list of categories</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<CategoryDto>>> GetCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Get a category by id
        /// </summary>
        /// <param name="id">Id of the category</param>
        /// <returns>A category</returns>
        /// <response code="200">Returns the category</response>
        /// <response code="404">If the category was not found</response>
        /// <response code="400">If the id is not valid</response>
        /// <response code="500">If an error occurred</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/categories/1
        /// </remarks>
        [HttpGet("{id:int}", Name = "GetCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        /// <summary>
        /// Add a new category
        /// </summary>
        /// <param name="category">Category to add</param>
        /// <returns>The added category</returns>
        /// <response code="201">Returns the added category</response>
        /// <response code="400">If the category is not valid</response>
        /// <response code="500">If an error occurred</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/categories
        ///     {
        ///         "name": "New Category"
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> AddCategoryAsync([FromBody] CategoryCreateDto categoryCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addedCategory = await _categoryService.AddCategoryAsync(categoryCreateDto);

                return CreatedAtRoute(
                    "GetCategoryAsync",
                    new { id = addedCategory.Id },
                    addedCategory
                );
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}