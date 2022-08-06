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
    public partial class UsersTable
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private bool _isBusy;
        private string _errorMessage = string.Empty;
        private IEnumerable<UserDto> _users = new List<UserDto>();

        private async Task GetUsersAsync()
        {
            _isBusy = true;
            try
            {
                var resutl = await UserService.GetAllUsersAsync();
                _users = resutl;
            }
            catch (Exception ex)
            {

                _errorMessage = ex.Message;
            }
            _isBusy = false;
        }
        private void ViewUser(UserDto user)
        {
            Navigation.NavigateTo($"/Users/{user.Id}");
        }

        private async void DeleteUser(UserDto user)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really want to delete {user.FirstName}?");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                try
                {
                    await UserService.DeleteUserAsync(user.Id);
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

            await GetUsersAsync();
        }
    }
}