using AKSoftware.Blazor.Utilities;
using BlazorWasmPlanner.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Components
{
    public partial class PlanCardsList
    {
        private bool _isBusy { get; set; }
        private int _pageNumber = 1;
        private int _pageSize = 10;
        private string _query = string.Empty;

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public Func<string, int, int, Task<PagedList<PlanSummary>>> FetchPlans { get; set; }

        [Parameter]
        public EventCallback<PlanSummary> OnViewClicked { get; set; }

        [Parameter]
        public EventCallback<PlanSummary> OnEditClicked { get; set; }

        [Parameter]
        public EventCallback<PlanSummary> OnDeleteClicked { get; set; }

        private PagedList<PlanSummary> _result = new();

        protected override void OnInitialized()
        {
            MessagingCenter.Subscribe<PlansList, PlanSummary>(this, "plan_deleted", async (sender, args) =>
            {
                await GetPlansAsync(_pageNumber);
                StateHasChanged();
            });
        }

        protected async override Task OnInitializedAsync()
        {
            await GetPlansAsync();
        }

        private async Task GetPlansAsync(int pageNumber = 1)
        {
            _pageNumber = pageNumber;
            _isBusy = true;
            _result = await FetchPlans?.Invoke(_query, _pageNumber, _pageSize);
            _isBusy = false;
        }

    }
}
