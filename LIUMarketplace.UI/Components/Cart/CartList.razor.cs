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
    public partial class CartList
    {
        [Inject]
        public ICartService CartService { get; set; }


        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Parameter]
        public ProductDetailsDto Product { get; set; }


        [Inject]
        public IDialogService DialogService { get; set; }

        [Parameter]
        public CartItemDto CartItem { get; set; }

        private IEnumerable<CartItemDto> _items = new List<CartItemDto>();

        private IEnumerable<ProductDetailsDto> _products = new List<ProductDetailsDto>();

       

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private async Task GetItemsAsync()
        {
            _isBusy = true;
            try
            {
                var result = await ProductService.GetProductsByCartAsync();
                _products = result;
                var items = await CartService.GetCartItemsAsync();
                _items = items;
            }
            catch (Exception ex)
            {

                _errorMessage = ex.Message;
            }
            _isBusy = false;
        }

        private async Task DeleteItemAsync(ProductDetailsDto product)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really want to Remove {product.Name} from shopping cart?");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                try
                {
                    await CartService.RemoveItemToCartAsync(product.Id);
                    await OnInitializedAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
        private async Task ContactOwnerAsync(ProductDetailsDto product, string firstName, string lastName, string email)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really want to Contact the owner");
            parameters.Add("ButtonText", "Yes");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Yes", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                
                try
                {
                    var owner = await UserService.GetByProductIdAsync(product.Id);
                    
                    var request = new ConnectionRequest
                    {
                        SenderEmail =  email,
                        SenderName = $"{firstName} {lastName}",
                        ReseverName = $"{owner.FirstName} {owner.LastName}",
                        MailTo = owner.Email, 
                        Subject = "Connection Request",
                        ProductName = product.Name,
                    };

                    await CartService.ContactOwnerAsync(request);
                    await OnInitializedAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
        

        protected override async Task OnInitializedAsync()
        {
            await GetItemsAsync();
            
        }
    }

}