using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

public interface ICategoriesService
{
    Task<Result<NewCategoryResponseDto>> AddCategory(string categoryName);

    Task DeleteCategories(List<int> Ids);

    Task<CategoriesDto> GetCategories();
}