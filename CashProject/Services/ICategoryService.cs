using CashProject.Models;

namespace CashProject.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAllAsync();
        Task InsertAsync(CategoryViewModel categoryViewModel);

    }
}
