using FreeCodeCampAcademy.Assets.VComponents;
using FreeCodeCampAcademy.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FreeCodeCampAcademy.ViewComponents;

public class GenderDDL : ViewComponent
{
    public GenderDDL()
    {

    }

    public async Task<IViewComponentResult> InvokeAsync (int selectedId)
    {
        List<NameValueObject>  items await LoadItemsAsync();
        var retItem = new GenderVCM { ItemList = items, Gender = selectedId };
        return View(retItem);
    }

    private async Task<List<NameValueObject>> LoadItemsAsync()
    {
        try
        {
            List<NameValueObject> items = typeof(MaritalStatus).ToDesList();
            if (items == null || items.Count < 1)
            {
                return [];
            }

            return await Task.FromResult(items);
        }
        catch (Exception ex)
        {

            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return [];
        }
    }
}
