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
        private List<ToDoItemDetail> _items = new();

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
                _items = _plan.ToDoItems;
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

        private void OnToDoItemAddedCallback(ToDoItemDetail todoItem)
        {
            _items.Add(todoItem);
        }

        private void OnToDoItemDeletedCallback(ToDoItemDetail todoItem)
        {
            _items.Remove(todoItem);
        }

        private void OnToDoItemEditedCallback(ToDoItemDetail todoItem)
        {
            var editedItem = _items.SingleOrDefault(i => i.Id == todoItem.Id);
            editedItem.Description = todoItem.Description;
            editedItem.IsDone = todoItem.IsDone;
        }

    }
}
