using Microsoft.AspNetCore.Http;

namespace BlazorWasmPlanner.Shared.Models
{
    public class PlanDetail : PlanSummary
    {
        public IFormFile CoverFile { get; set; }
        // List of the todos
    }
}
