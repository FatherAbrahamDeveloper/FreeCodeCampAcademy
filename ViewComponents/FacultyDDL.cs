using FreeCodeCampAcademy.APIService.APIContracts;
using Microsoft.AspNetCore.Mvc;

namespace FreeCodeCampAcademy.ViewComponents;

public class FacultyDDL(ILookupService lookup) : ViewComponent
{
    private readonly ILookupService _lookupService = lookup;

    public async Task<IViewComponentResult> InvokeAsync(int selectedId)
    {
        List<NameValueObject> items = await LoadItemsAsync();
        var retItem = new FacultyVCM { ItemList = items, FacultyId = selectedId };
        return View(retItem);
    }

    private async Task<List<NameValueObject>> LoadItemsAsync()
    {
        try
        {

            var result = await _lookupService.GetFacultiesAsync();
            if (result.Failed || result.Value == null)
            {
                return [];
            }
            var list = result.Value.OrderBy(m => m.Name).ToList();
            List<NameValueObject> items = new List<NameValueObject>();
            foreach (var item in list)
            {
                items.Add(new NameValueObject
                {
                    Id = item.FacultyId,
                    Name = item.Name,
                });
            }

            if (items == null || items.Count < 1)
            {
                return [];
            }

            return items;
        }
        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return [];
        }

    }
}

