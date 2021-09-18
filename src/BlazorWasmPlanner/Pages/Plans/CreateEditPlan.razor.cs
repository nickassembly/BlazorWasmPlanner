using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Pages.Plans
{
    public partial class CreateEditPlan
    {
        [Parameter]
        public string Id { get; set; }

        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/index"),
            new BreadcrumbItem("Plans", "/plans"),
            new BreadcrumbItem("Create", "/plans/form", true)
        };
    }
}
