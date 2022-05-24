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
    public partial class ProductDetails
    {
        [Inject]
        public IProductService ProudctService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public ProductDetailsDto Product { get; set; }

        [Parameter]
        public EventCallback<ProductDetailsDto> OnAddToCartClicked { get; set; }

        [Parameter]
        public EventCallback<ProductDetailsDto> OnAddToFavoriteClicked { get; set; }

        private ProductDetailsDto _product = new();

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private async Task GetProductByIdAsync()
        {
            _isBusy = true;
            try
            {
                var result = await ProudctService.GetProductByIdAsync(Id);
                _product = result;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }

            _isBusy = false;
        }
        protected override async Task OnInitializedAsync()
        {
            await GetProductByIdAsync();
        }
    }
}