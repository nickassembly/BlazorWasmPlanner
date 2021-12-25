using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BlazorWasmPlanner.Shared.Models
{
    public class PlanDetail : PlanSummary
    {
        public IFormFile CoverFile { get; set; }
        // List of the todos
        public List<ToDoItemDetail> ToDoItems { get; set; }
    }

}
