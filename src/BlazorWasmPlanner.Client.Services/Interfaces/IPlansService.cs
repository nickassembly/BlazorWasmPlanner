using BlazorWasmPlanner.Shared.Models;
using BlazorWasmPlanner.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Client.Services.Interfaces
{
    public interface IPlansService
    {
        Task<ApiResponse<PagedList<PlanSummary>>> GetPlansAsync(string query = null, int pageNumber = 1, int pageSize = 10);
    }
}
