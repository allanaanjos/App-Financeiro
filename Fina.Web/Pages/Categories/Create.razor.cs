using Fina.Core.Handler;
using Fina.Core.Requests.Categoria;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Categories
{
    public partial class CreateCategoryPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public CreateCategoryRequest InputModel { get; set; } = new();

        #endregion

        #region Service

        [Inject]
        public ICategoryHandler handler { get; set; } = null!;

        [Inject]
        public NavigationManager navigation { get; set; } = null!;

        [Inject]
        public ISnackbar snackbar { get; set; } = null!;

        #endregion

        #region Methods
        public async Task OnValidSubmitAsync()
        {
            try
            {
                IsBusy = true;
                var results = await handler.CreateAsync(InputModel);
                if(results.IsSuccess)
                {
                    snackbar.Add(results.Message, Severity.Success);
                    navigation.NavigateTo("/categorias");
                }
                else
                  snackbar.Add(results.Message, Severity.Error);

            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
               IsBusy = false;
            }

        }

        #endregion
    }

}