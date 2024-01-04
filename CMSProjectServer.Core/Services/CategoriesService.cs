using CMSProjectServer.DAL;
using CMSProjectServer.Domain.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

internal class CategoriesService : ICategoriesService
{
    private readonly CMSDbContext dbContext;

    public CategoriesService(CMSDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<CategoriesDto> GetCategories()
    {
        var categories = await dbContext.Categories.ToListAsync();
        var response = new CategoriesDto()
        {
            Categories = categories.Select(x => new CategoryDto() { Id = x.Id, Category = x.Category }).ToList(),
        };
        return response;
    }

    public async Task<NewCategoryResponseDto> AddCategory(string categoryName)
    {
        var category = new Domain.Entities.ArticleCategory { Category = categoryName };
        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync();
        return new NewCategoryResponseDto() { Id = category.Id };
    }

    public async Task DeleteCategories(List<int> Ids)
    {
        var existing = await dbContext.Categories.Where(x => Ids.Contains(x.Id)).ToListAsync();
        dbContext.RemoveRange(existing);
        await dbContext.SaveChangesAsync();
    }
}