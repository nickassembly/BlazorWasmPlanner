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

        private PlanDetail _model = new PlanDetail();
        private bool _isBusy = false;
        private Stream _stream = null;
        private string _fileName = string.Empty;
        private string _errorMessage = string.Empty;

        private async Task SubmitFormAsync()
        {
            _errorMessage = "done";
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
