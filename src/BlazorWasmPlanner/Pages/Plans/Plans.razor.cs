using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Pages.Plans
{
    public partial class Plans
    {
        private List<BreadcrumbItem> _breadcrumbItems { get; set; } = new()
        {
            new BreadcrumbItem("Home", "/index"),
            new BreadcrumbItem("Plans", "/plans", true),
        };
    }
}
