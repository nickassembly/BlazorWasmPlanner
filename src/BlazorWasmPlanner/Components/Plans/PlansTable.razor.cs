using BlazorWasmPlanner.Client.Services.Interfaces;
using BlazorWasmPlanner.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Components
{
    public partial class PlansTable
    {
        [Inject]
        public IPlansService PlansService { get; set; }

        [Parameter]
        public EventCallback<PlanSummary> OnViewClicked { get; set; }

        [Parameter]
        public EventCallback<PlanSummary> OnDeleteClicked { get; set; }

        [Parameter]
        public EventCallback<PlanSummary> OnEditClicked { get; set; }

        private string _query = string.Empty;
        private MudTable<PlanSummary> _table;

        private async Task<TableData<PlanSummary>> ServerReloadAsync(TableState state)
        {
            var result = await PlansService.GetPlansAsync(_query, state.Page, state.PageSize);

            return new TableData<PlanSummary>
            {
                Items = result.Value.Records,
                TotalItems = result.Value.ItemsCount
            };
        }

        private void OnSearch(string query)
        {
            _query = query;
            _table.ReloadServerData();
        }


    }
}
