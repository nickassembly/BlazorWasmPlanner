using AKSoftware.Blazor.Utilities;
using BlazorWasmPlanner.Client.Services.Exceptions;
using BlazorWasmPlanner.Client.Services.Interfaces;
using BlazorWasmPlanner.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PlannerApp.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Components
{
    public partial class PlansList
    {
        [Inject]
        public IPlansService PlansService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private bool _isBusy;
        private string _errorMessage = string.Empty;
        private int _pageNumber = 1;
        private int _pageSize = 10;
        private string _query = string.Empty;
        private int _totalPages = 1;

        private List<PlanSummary> _plans = new();


        private async Task<PagedList<PlanSummary>> GetPlansAsync(string query = "", int pageNumber = 1, int pageSize = 10)
        {
            _isBusy = true;

            try
            {
                var result = await PlansService.GetPlansAsync(query, pageNumber, pageSize);

                _plans = result.Value.Records.ToList();
                _pageNumber = result.Value.Page;
                _pageSize = result.Value.PageSize;
                _totalPages = result.Value.TotalPages;

                return result.Value;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {

                // TODO: Log error
                _errorMessage = ex.Message;
            }
            _isBusy = false;
            return null;
        }

        private bool _isCardsViewEnabled = true;

        private void SetCardsView()
        {
            _isCardsViewEnabled = true;
        }

        private void SetTableView()
        {
            _isCardsViewEnabled = false;
        }

        private void EditPlan(PlanSummary plan)
        {
            Navigation.NavigateTo($"/plans/form/{plan.Id}");
        }

        private async Task DeletePlanAsync(PlanSummary plan)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really want to delete the plan '{plan.Title}' ? This process cannot be undone.");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Delete", parameters, options);
            var confirmationResult = await dialog.Result;

            if (!confirmationResult.Cancelled)
            {
                // Confirmed to delete
                try
                {
                    await PlansService.DeleteAsync(plan.Id);

                    // Send a message about the deleted plan
                    MessagingCenter.Send(this, "plan_deleted", plan);
                }
                catch (ApiException ex)
                {
                    // TODO: Log this error
                }
                catch (Exception ex)
                {
                    // TODO: Log this error
                }
                
            }
        }

        private void ViewPlan(PlanSummary plan)
        {
            var parameters = new DialogParameters();
            parameters.Add("PlanId", plan.Id);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = DialogService.Show<PlanDetailsDialog>("Details", parameters, options);
          
        }

    }
}
