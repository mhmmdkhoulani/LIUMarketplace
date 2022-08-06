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
using AKSoftware.Blazor.Utilities;

namespace LIUMarketplace.UI.Components
{
    public partial class ProductsList
    {
        [Inject]
        public IProductService ProudctService { get; set; }

        [Inject]
        public ICategoryService CategoryService { get; set; }

        [Inject]
        public ICartService CartService { get; set; }

        [Inject]
        public NavigationManager Navigation { get;set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Parameter]
        public string Heading { get; set; }

        [Parameter]
        public bool ByCampus { get; set; }

        [Parameter]
        public string Campus { get; set; }

        [Parameter]
        public bool ByCategory { get; set; }

        [Parameter]
        public int CategoryId { get; set; } 


        private IEnumerable<ProductDetailsDto> _products = new List<ProductDetailsDto>();
        private CategoryDto _category = new();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        public int spacing { get; set; } = 2;
        private async Task GetProductsAsync()
        {
            _isBusy = true;
            if (ByCampus)
            {
                try
                {
                    var result = await ProudctService.GetProductsByCampusAsync(Campus);
                    _products = result;
                }
                catch (Exception ex)
                {
                    _errorMessage = ex.Message;
                }
            }
            else if (ByCategory)
            {
                try
                {
                    var result = await ProudctService.GetProductsByCategoryAsync(CategoryId);
                    _products = result;
                }
                catch (Exception ex)
                {
                    _errorMessage = ex.Message;
                }
            }
            else
            {
                try
                {
                    var result = await ProudctService.GetProductsAsync();
                    _products = result;
                }
                catch (Exception ex)
                {
                    _errorMessage = ex.Message;
                }
            }
            

            _isBusy = false;
        }

        private async Task GetCategory()
        {
            try
            {
                var result = await CategoryService.GetCategoryByIdAsync(CategoryId);
                _category = result;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
        }
        private void EditProduct(ProductDetailsDto product)
        {
            Navigation.NavigateTo($"/products/form/{product.Id}");
        }

        private void ViewProduct(ProductDetailsDto product)
        {
            Navigation.NavigateTo($"/product/{product.Id}");
        }

        private async void DeleteProduct(ProductDetailsDto product)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really want to delete {product.Name}?");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Delete", parameters, options);
            var result = await dialog.Result;
            if(!result.Cancelled)
            {
                try
                {
                    await ProudctService.DeleteProductAsync(product.Id);
                    await OnInitializedAsync();
                }
                catch ( Exception ex)
                {

                    throw ;
                }

            }
        }
        private async void AddItemToCart(ProductDetailsDto product)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really Add {product.Name} to the cart?");
            parameters.Add("ButtonText", "Add");
            parameters.Add("Color", Color.Primary);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Add", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var cart = await CartService.GetCartAsync();
                var item = new CartItemDto
                {
                    CartId = cart.CartId,
                    ProductId = product.Id,
                };

                try
                {
                    await CartService.AddItemToCartAsync(item);
                    Navigation.NavigateTo($"/cart");

                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }

        protected override async Task OnInitializedAsync()
        {
            if(CategoryId > 0)
                await GetCategory();

            await GetProductsAsync();
        }
    }
}