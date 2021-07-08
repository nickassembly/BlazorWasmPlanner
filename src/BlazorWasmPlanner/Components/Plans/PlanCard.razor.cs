using BlazorWasmPlanner.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmPlanner.Components
{
    public partial class PlanCard
    {
        [Parameter]
        public bool IsBusy { get; set; }

        [Parameter]
        public PlanSummary PlanSummary { get; set; }

        [Parameter]
        public EventCallback<PlanSummary> OnViewClicked { get; set; }

        [Parameter]
        public EventCallback<PlanSummary> OnDeleteClicked { get; set; }

        [Parameter]
        public EventCallback<PlanSummary> OnEditClicked { get; set; }

       
    }
}
