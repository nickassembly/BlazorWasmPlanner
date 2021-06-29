using BlazorWasmPlanner.Client.Services.Exceptions;
using BlazorWasmPlanner.Client.Services.Interfaces;
using BlazorWasmPlanner.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Components.Authentication
{
    public partial class RegisterForm : ComponentBase
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        private RegisterRequest _model = new();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private async Task RegisterUserAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;

            try
            {
                await AuthenticationService.RegisterUserAsync(_model);
                Navigation.NavigateTo("/authentication/login");
            }
            catch (ApiException ex)
            {
                // Handle api errors
                // TODO: Log those errors
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch(Exception ex)
            {
                // Handle General errors
                _errorMessage = ex.Message;
            }

            _isBusy = false;
        }

        private void RedirectToLogin()
        {
            Navigation.NavigateTo("/authentication/login");
        }
    }
}

