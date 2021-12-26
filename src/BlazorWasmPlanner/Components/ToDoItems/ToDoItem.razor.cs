using BlazorWasmPlanner.Client.Services.Exceptions;
using BlazorWasmPlanner.Client.Services.Interfaces;
using BlazorWasmPlanner.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Components
{
    public partial class ToDoItem
    {
        [Inject]
        public IToDoItemsService ToDoItemService { get; set; }

        [Parameter]
        public ToDoItemDetail Item { get; set; }

        [Parameter]
        public EventCallback<ToDoItemDetail> OnItemDeleted { get; set; }

        [Parameter]
        public EventCallback<ToDoItemDetail> OnItemEdited { get; set; }

        private bool _isChecked = true;

        private bool _isEditMode = false;

        private bool _isBusy = false;

        private string _description = string.Empty;
        private string _errorMessage = string.Empty;

        private void ToggleEditMode(bool isCancel)
        {
            if (_isEditMode)
            {
                _isEditMode = false;
                _description = isCancel ? Item.Description : _description;
            }
            else
            {
                _isEditMode = true;
                _description = Item.Description;
            }
        }

        private async Task RemoveItemAsync()
        {
            try
            {
                _isBusy = true;
                // Call the Api to add the item
                await ToDoItemService.DeleteAsync(Item.Id);

                // Notify the parent about the newly added item
                await OnItemDeleted.InvokeAsync(Item);
            }
            catch (ApiException ex)
            {
                // TODO: Handle errors globally
            }
            catch (Exception ex)
            {
                // TODO: Handle errors globally
            }

            _isBusy = false;
        }

        private async Task EditItemAsync()
        {
            _errorMessage = string.Empty; 

            try
            {
                if (string.IsNullOrWhiteSpace(_description))
                {
                    _errorMessage = "Description is required.";
                }

                _isBusy = true;
                // Call the Api to add the item
                var result = await ToDoItemService.EditAsync(Item.Id, _description, Item.PlanId);
                ToggleEditMode(false);

                // Notify the parent about the newly added item
                await OnItemEdited.InvokeAsync(result.Value);
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                // TODO: Handle errors globally
            }

            _isBusy = false;
        }

    }
}
