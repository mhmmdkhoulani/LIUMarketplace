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
    public partial class ProuductsList
    {
        [Inject]
        public HttpClient HttpClient { get; set; }
        private IEnumerable<ProductDto> _models = new List<ProductDto>();

        private bool _isBusy = false;

        //private async Task<IEnumerable<ProductDto>> GetProductsAsync()
        //{
        //    _isBusy = true;
           

        //    var response = await HttpClient.GetAsync("/api/products/");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var result = await response.Content.ReadFromJsonAsync<ProductDto>();
        //        return result;
        //    }
        //    else
        //    {
        //        var errorResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        //        _errorMessage = errorResponse.Messages;
        //    }
        //    _isBusy = false;
        //}
    }
}