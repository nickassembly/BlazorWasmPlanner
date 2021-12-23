using BlazorWasmPlanner.Shared.Models;
using BlazorWasmPlanner.Shared.Responses;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Client.Services.Interfaces
{
    public interface IToDoItemsService
    {
        Task<ApiResponse<ToDoItemDetail>> CreateAsync(string description, string planId);
        Task<ApiResponse<ToDoItemDetail>> EditAsync(string id, string newDescription, string planId);
        Task ToggleAsync(string id);
        Task DeleteAsync(string id);
    }
}
