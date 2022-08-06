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
using LIUMarketplace.UI.Service;
using LIUMarketplace.Shared.DTOs;
using LIUMarketplace.UI.Service.Interfaces;

namespace LIUMarketplace.UI.Components
{
    public partial class CategoriesTable
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private bool _isBusy;
        private string _errorMessage = string.Empty;
        private IEnumerable<CategoryDto> _categories = new List<CategoryDto>();

        private async Task GetCategoriesAsync()
        {
            _isBusy = true;
            try
            {
                var resutl = await CategoryService.GetCategoriesAsync();
                _categories = resutl;
            }
            catch (Exception ex)
            {

                _errorMessage = ex.Message;
            }
            _isBusy = false;
        }

        private void EditCategory(CategoryDto category)
        {
            Navigation.NavigateTo($"/categories/form/{category.Id}");
        }

        private async void DeleteCategory(CategoryDto category)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really want to delete {category.Name}?");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                try
                {
                    await CategoryService.DeleteCategoryAsync(category.Id);
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

            await GetCategoriesAsync();
        }
    }

}