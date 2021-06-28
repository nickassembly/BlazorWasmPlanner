using BlazorWasmPlanner.Shared.Models;
using BlazorWasmPlanner.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Client.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ApiResponse> RegisterUserAsync(RegisterRequest model);

        // TODO: migration login to IAuthenticationService
    }
}
