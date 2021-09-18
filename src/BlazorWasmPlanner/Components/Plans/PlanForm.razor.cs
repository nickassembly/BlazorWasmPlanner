using BlazorWasmPlanner.Client.Services.Exceptions;
using BlazorWasmPlanner.Client.Services.Interfaces;
using BlazorWasmPlanner.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Components
{
    public partial class PlanForm
    {
        [Inject]
        public IPlansService PlansService { get; set; }

        [Parameter]
        public string Id { get; set; }

        private bool _isEditMode => Id != null;

        [Inject]
        public NavigationManager Navigation { get; set; }

        private PlanDetail _model = new PlanDetail();
        private bool _isBusy = false;
        private Stream _stream = null;
        private string _fileName = string.Empty;
        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (_isEditMode)
                await FetchPlanByIdAsync();
        }

        private async Task SubmitFormAsync()
        {
            _isBusy = true;

            try
            {
                FormFile formFile = null;

                if (_stream != null)
                    formFile = new FormFile(_stream, _fileName);

                if (_isEditMode)
                    await PlansService.EditAsync(_model, formFile);
                else
                    await PlansService.CreateAsync(_model, formFile);

                // Success
                Navigation.NavigateTo("/plans");
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {

                // TODO: Log the error
                _errorMessage = ex.Message;
            }

            _isBusy = false;
        }

        private async Task FetchPlanByIdAsync()
        {
            _isBusy = true;

            try
            {
                var result = await PlansService.GetByIdAsync(Id);
                _model = result.Value;
            }
            catch (ApiException ex)
            {
              _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                // TODO: Log the error
                _errorMessage = ex.Message;
            }

            _isBusy = false;
        }

        private async Task OnChooseFileAsync(InputFileChangeEventArgs e)
        {
            _errorMessage = string.Empty;
            var file = e.File;
            if (file != null)
            {
                if (file.Size > 2097152)
                {
                    _errorMessage = "The file must be less than 2MB";
                    return;
                }

                string[] allowedExtensions = new[] { ".jpg", ".png", ".bmp", ".svg" };

                string extension = Path.GetExtension(file.Name).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    _errorMessage = "Please choose a valid image type (.jpg/.png/.bmp/.svg)";
                    return;
                }

                using (var stream = file.OpenReadStream(2097152))
                {
                    var buffer = new byte[file.Size];
                    await stream.ReadAsync(buffer, 0, (int)file.Size);
                    _stream = new MemoryStream(buffer);
                    _stream.Position = 0;
                    _fileName = file.Name;
                }
            }
        }
    }
}
