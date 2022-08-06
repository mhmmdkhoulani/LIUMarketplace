using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LIUMarketplace.Shared.DTOs;
using LIUMarketplace.UI.Service.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace LIUMarketplace.UI.Components
{
    public partial class CategoryForm
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public ICategoryService CategoryService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        
        private bool _isEditMode => Id > 0; 

        private CategoryDto _model = new CategoryDto();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private async Task SubmitFormAsync()
        {
            _isBusy = true;
            try
            {
                if (_isEditMode)
                {
                    _model.Id = Id;
                    var result = await CategoryService.UpdateCategoryAsync(_model);
                    NavigationManager.NavigateTo("/categories");


                }
                else
                {
                    var result = await CategoryService.AddCategoryAsync(_model);
                    NavigationManager.NavigateTo("/categories");

                }
            }
            catch (Exception ex)
            {

                _errorMessage = ex.Message;
            }
            _isBusy = false;
        }



        private async Task FetchCategoryByIdAsync()
        {
            _isBusy = true;
            try
            {
                var result = await CategoryService.GetCategoryByIdAsync(Id);
                _model.Name = result.Name;


            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            _isBusy = false;

        }


        protected override async Task OnInitializedAsync()
        {
           
            if(_isEditMode)
                await FetchCategoryByIdAsync();
        }
    }
}