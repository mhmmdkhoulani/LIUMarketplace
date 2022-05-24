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
    public partial class ProductsList
    {
        [Inject]
        public IProductService ProudctService { get; set; }

        private IEnumerable<ProductDetailsDto> _products = new List<ProductDetailsDto>();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        public int spacing { get; set; } = 2;
        private async Task GetProductsAsync()
        {
            _isBusy = true;
            try
            {
                var result = await ProudctService.GetProductsAsync();
                _products = result;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }

            _isBusy = false;
        }

        protected override async Task OnInitializedAsync()
        {
            await GetProductsAsync();
        }
    }
}