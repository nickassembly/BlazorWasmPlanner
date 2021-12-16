using BlazorWasmPlanner.Client.Services.Exceptions;
using BlazorWasmPlanner.Client.Services.Interfaces;
using BlazorWasmPlanner.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Components
{
    public partial class PlanDetailsDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Inject]
        public IPlansService PlansService { get; set; }

        [Parameter]
        public string PlanId { get; set; }

        private PlanDetail _plan;
        private bool _isBusy;
        private string _errorMessage = String.Empty; 

        private void Close()
        {
            MudDialog.Cancel();    
        }

        protected override void OnParametersSet()
        {
            if (PlanId == null)
                throw new ArgumentNullException(nameof(PlanId));

            base.OnParametersSet();
        }

        protected async override Task OnInitializedAsync()
        {
            await FetchPlanAsync();
        }

        private async Task FetchPlanAsync()
        {
            _isBusy = true;

            try
            {
                var result = await PlansService.GetByIdAsync(PlanId);
                _plan = result.Value;
                StateHasChanged();
            }
            catch (ApiException ex)
            {
                // TODO
            }
            catch (Exception ex)
            {
                // Log this error
            }
            _isBusy = false;
        }

    }
}
