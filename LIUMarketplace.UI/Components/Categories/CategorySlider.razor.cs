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
using LIUMarketplace.UI;
using LIUMarketplace.UI.Components;
using LIUMarketplace.UI.Shared;
using MudBlazor;
using LIUMarketplace.UI.Service.Interfaces;
using LIUMarketplace.Shared.DTOs;

namespace LIUMarketplace.UI.Components
{
    public partial class CategorySlider
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }

        private IEnumerable<CategoryDto> _categories = new List<CategoryDto>();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        public int spacing { get; set; } = 2;
        private async Task GetCategoriesAsync()
        {
            _isBusy = true;
            try
            {
                var result = await CategoryService.GetCategoriesAsync();
                _categories = result;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }

            _isBusy = false;
        }

        protected override async Task OnInitializedAsync()
        {
            await GetCategoriesAsync();
        }
    }
}