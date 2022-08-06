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
using BlazorComponentUtilities;

namespace LIUMarketplace.UI.Components
{
    public partial class CampusSlider
    {
        public int spacing { get; set; } = 2;
        private string _textStyle => new StyleBuilder()
                                         .AddStyle("cursor", "pointer")
                                         .Build();
        

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private void OnViewEvent(string campus)
        {
            NavigationManager.NavigateTo($"/products/campus/{campus}");
        }


    }
}