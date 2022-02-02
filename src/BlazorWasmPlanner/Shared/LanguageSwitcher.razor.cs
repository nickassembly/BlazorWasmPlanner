using AKSoftware.Localization.MultiLanguages;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Shared
{
    public partial class LanguageSwitcher
    {
        [Inject]
        public ILanguageContainerService Language { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (await LocalStorage.ContainKeyAsync("language"))
            {
                string cultureCode = await LocalStorage.GetItemAsStringAsync("language");
                Language.SetLanguage(CultureInfo.GetCultureInfo(cultureCode));
            }
           
        }

        private async Task ChangeLanguageAsync(string cultureCode)
        {
            Language.SetLanguage(CultureInfo.GetCultureInfo(cultureCode));

            await LocalStorage.SetItemAsStringAsync("language", cultureCode);
        }


    }
}
