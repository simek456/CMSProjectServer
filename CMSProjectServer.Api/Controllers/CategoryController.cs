using CMSProjectServer.Core.Services;
using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSProjectServer.Api.Controllers;

[Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoriesService categoriesService;

    public CategoryController(ICategoriesService articleService)
    {
        this.categoriesService = articleService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCategories()
    {
        var result = await categoriesService.GetCategories();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] NewCategoryDto newCategory)
    {
        var result = await categoriesService.AddCategory(newCategory.Category);
        return Ok(result);
    }

    [HttpDelete()]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> DeleteCategory([FromQuery] List<int> categoryIds)
    {
        await categoriesService.DeleteCategories(categoryIds);
        return Ok();
    }
}