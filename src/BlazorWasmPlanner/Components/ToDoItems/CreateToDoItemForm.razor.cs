using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using BlazorWasmPlanner;
using BlazorWasmPlanner.Shared;
using BlazorWasmPlanner.Components;
using MudBlazor;
using Blazored.FluentValidation;
using BlazorWasmPlanner.Client.Services.Interfaces;
using BlazorWasmPlanner.Client.Services.Exceptions;
using BlazorWasmPlanner.Shared.Models;

namespace BlazorWasmPlanner.Components
{
    public partial class CreateToDoItemForm
    {
        [Inject]
        public IToDoItemsService ToDoItemService { get; set; }

        [Parameter]
        public string PlanId { get; set; }

        [Parameter]
        public EventCallback<ToDoItemDetail> OnToDoItemAdded { get; set; }

        private bool _isBusy = false;
        private string _description { get; set; }
        private string _errorMessage = string.Empty;

        private async Task AddToDoItemAsync()
        {
            _errorMessage = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(_description))
                {
                    _errorMessage = "Description is required";
                    return;
                }

                _isBusy = true;
                // Call the Api to add the item
                var result = await ToDoItemService.CreateAsync(_description, PlanId);
                _description = String.Empty;
                
                // Notify the parent about the newly added item
                await OnToDoItemAdded.InvokeAsync(result.Value);
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