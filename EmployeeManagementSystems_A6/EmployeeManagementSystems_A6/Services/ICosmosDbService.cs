// Services/ICosmosDbService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementSystems_A6.Models;

namespace EmployeeManagementSystems_A6.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<T>> GetItemsAsync<T>(string query);
        Task<T> GetItemAsync<T>(string id) where T : BaseEntity;
        Task AddItemAsync<T>(T item) where T : BaseEntity;
        Task UpdateItemAsync<T>(string id, T item) where T : BaseEntity;
        Task DeleteItemAsync(string id);
    }
}
