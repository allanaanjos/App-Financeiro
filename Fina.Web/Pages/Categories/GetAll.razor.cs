using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categoria;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Categories;

public partial class GetAllCategoriesPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public List<Category> Categories { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;

    #endregion

    #region Services

    [Inject]
    public ICategoryHandler handler { get; set; } = null!;

    [Inject]
    public IDialogService Dialog { get; set; } = null!;

    [Inject]
    public ISnackbar snackbar { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetAllCategoriesRequest();
            var result = await handler.GetAllAsync(request);
            if (result.IsSuccess)
                Categories = result.Data ?? [];
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


    public async void OnDeleteButtonAsync(long id, string title)
    {
        var result = await Dialog.ShowMessageBox(
            "ATENÇÂO",
            $"Ao prosseguir a categoria {title} será removida dejesa continuar? ",
            yesText: "Excluir",
            cancelText: "Cancelar"

        );

        if (result is true)
            await OnDeleteAsync(id, title);

        StateHasChanged();

    }

    public async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            var request = new DeleteCategoryRequest
            {
                Id = id
            };
            await handler.DeleteAsync(request);
            Categories.RemoveAll(x => x.Id == id);
            snackbar.Add($"Categoria {title} removida", Severity.Info);
        }
        catch (Exception ex)
        {
            snackbar.Add(ex.Message, Severity.Error);
        }
    }
}
