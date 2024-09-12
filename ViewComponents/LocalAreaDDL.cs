using FreeCodeCampAcademy.APIService.APIContracts;
using Microsoft.AspNetCore.Mvc;

namespace FreeCodeCampAcademy.ViewComponents;

public class LocalAreaDDL(ILookupService lookup) : ViewComponent
{
    private readonly ILookupService _lookupService = lookup;

    public async Task<IViewComponentResult> InvokeAsync(int stateId, int selectedId)
    {
        List<NameValueObject> items = await LoadItemsAsync(stateId);
        var retItem = new LocalAreaVCM { ItemList = items, LocalAreaId = selectedId, StateId = stateId };
        return View(retItem);
    }

    private async Task<List<NameValueObject>> LoadItemsAsync(int stateId)
    {
        try
        {
            var result = await _lookupService.GetLocalAreasAsync(stateId);
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
                    Id = item.LocalAreaId,
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
