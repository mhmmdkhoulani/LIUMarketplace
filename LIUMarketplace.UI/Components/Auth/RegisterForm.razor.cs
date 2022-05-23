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
using Blazored.LocalStorage;
using LIUMarketplace.Shared.DTOs;
using LIUMarketplace.UI.Service.Interfaces;
using LIUMarketplace.UI.Service.Exceptions;

namespace LIUMarketplace.UI.Components
{
    public partial class RegisterForm
    {
        [Inject]
        public IAuthenticationService Authentication { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }


        private RegisterDto _model = new RegisterDto();

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        public int spacing { get; set; } = 1;

        private async Task RegisterUserAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;

            try
            {
                await Authentication.RegisterUserAsync(_model);
                Navigation.NavigateTo("/auth/login");
            }
            catch (ApiException ex)
            {
                //handel error of the api
                _errorMessage = ex.AuthResponse.Messages;
            }
            catch(Exception ex)
            {
                //handle errors 
                _errorMessage = ex.Message;
            }
            _isBusy = false;
        }


        bool isShow;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        void ShowPassword()
        {
            if (isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }

    }
}