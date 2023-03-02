using CashProject.Models;
using Dapper;
using System.Data.SqlClient;

namespace CashProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration _configuration;
        public CategoryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<CategoryViewModel>> GetAllAsync()
        {
              Task.Delay(10000).Wait();
            using (var connection =new SqlConnection(_configuration.GetConnectionString("NortWindConnection")))
            {
                var sqlQuery = "SELECT * FROM dbo.Categories";
                var result = await connection.QueryAsync<CategoryViewModel>(sqlQuery);
                return result.ToList();
            }
        }

        public async Task InsertAsync(CategoryViewModel categoryViewModel)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("NortWindConnection")))
            {
                var sqlQuery = "INSERT dbo.Categories(CategoryName) VALUES(@categoryName)";

                await connection.ExecuteAsync(sqlQuery, new { categoryName=categoryViewModel.CategoryName });


            }

        }
    }
}
