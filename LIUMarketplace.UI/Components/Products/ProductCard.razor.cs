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
using LIUMarketplace.Shared.DTOs;

namespace LIUMarketplace.UI.Components
{
    public partial class ProductCard
    {
        [Parameter]
        public ProductDetailsDto ProductDetails { get; set; } = new();

        [Parameter]
        public EventCallback<ProductDetailsDto> OnViewClicked { get; set; }

        [Parameter]
        public EventCallback<ProductDetailsDto> OnEditClicked { get; set; }

        [Parameter]
        public EventCallback<ProductDetailsDto> OnAddToCartClicked { get; set; }

        [Parameter]
        public EventCallback<ProductDetailsDto> OnDeleteClicked { get; set; }

        //[Inject]
        //public JwtAuthenticattionStateProvider AuthenticattionStateProvider { get; set; }


        
        //private bool _isAuther => ProductDetails.Id.Equals(getUserId());

        //private async Task<string> getUserId()
        //{
        //    var user = (await AuthenticattionStateProvider.GetAuthenticationStateAsync()).User;
        //    var UserId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
        //    return UserId;
        //}
    }
}