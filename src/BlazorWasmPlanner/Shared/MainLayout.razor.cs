using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorWasmPlanner.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        bool _drawerOpen = true;
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected async override Task OnInitializedAsync()
        {
            _currentTheme = _darkTheme;
        }

        MudTheme _currentTheme = null;

        MudTheme _darkTheme = new MudTheme
        {
           Palette = new Palette
           {
               AppbarBackground = "#0097FF",
               AppbarText = "#FFFFFF",
               Primary = "#007CD1",
               TextPrimary = "#FFFFFF",
               Background = "#001524",
               TextSecondary = "#E2EEF6",
               DrawerBackground = "#093958",
               DrawerText = "#FFFFFF",
               Surface = "#093958",
               ActionDefault = "#0097FF",
               ActionDisabled = "#2F678C",
               TextDisabled = "#B0B0B0"

           }
        };
    }
}