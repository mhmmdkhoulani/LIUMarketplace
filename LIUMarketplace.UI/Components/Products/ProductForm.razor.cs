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
    public partial class ProductForm
    {
        [Inject]
        public IProductService ProductService { get; set; }
       
        [Inject]
        public ICategoryService CategoryService { get; set; }

        private ProductDto _model = new ProductDto();
        private IEnumerable<CategoryDto> _categories = new List<CategoryDto>();
        private bool _isBusy = false;
        private Stream _stream = null;
        private string _fileName = string.Empty;
        private string _errorMessage = string.Empty;
        private async Task SubmitFormAsync()
        {

        }

        private async Task GetCategoriesAsync()
        {
            try
            {
                var result = await CategoryService.GetCategoriesAsync();
                _categories = result;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
        }
        private async Task OnChooseFileAsync (InputFileChangeEventArgs e)
        {
            _errorMessage = string.Empty;
            var file = e.File;
            if(file != null)
            {
                if(file.Size > 2097152)
                {
                    _errorMessage = "The file must be equal or less than 2MB";
                    return;
                }
                string[] allowedExtensions = new[] { ".jpg", ".png", ",jpeg", ".svg" };
                string extension = Path.GetExtension(file.Name).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    _errorMessage = "Please choose a valid image file";
                    return;
                }

                using (var stream = file.OpenReadStream(2097152))
                {
                    var buffer = new byte[file.Size];
                    await stream.ReadAsync(buffer, 0, (int)file.Size);
                    _stream = new MemoryStream(buffer);
                    _stream.Position = 0;
                    _fileName = file.Name;
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await GetCategoriesAsync();
        }
    }
}