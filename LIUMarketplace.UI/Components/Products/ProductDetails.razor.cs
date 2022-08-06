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

        [Inject]
        public ICartService CartService { get; set; }

        [Inject]
        public IFavoriteService FavoriteSevice { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

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

        private async void AddItemToFavorite(ProductDetailsDto product)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really Add {product.Name} to Favotit List?");
            parameters.Add("ButtonText", "Add");
            parameters.Add("Color", Color.Primary);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Add", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var fav = await FavoriteSevice.GetFavoriteAsync();
                var item = new FavoriteItemDto
                {
                    FavoriteId = fav.FavoriteId,
                    ProductId = product.Id,
                };

                try
                {
                    await FavoriteSevice.AddItemToFavoriteAsync(item);
                    Navigation.NavigateTo($"/favorite");

                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
        protected override async Task OnInitializedAsync()
        {
            await GetProductByIdAsync();
        }
    }
}