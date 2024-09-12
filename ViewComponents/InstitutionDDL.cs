using FreeCodeCampAcademy.APIService.APIContracts;
using Microsoft.AspNetCore.Mvc;

namespace FreeCodeCampAcademy.ViewComponents;

public class InstitutionDDL(ILookupService lookup) : ViewComponent
{
    private readonly ILookupService _lookupService = lookup;

    public async Task<IViewComponentResult> InvokeAsync(int instType, int selectedId)
    {
        List<NameValueObject> items = await LoadItemsAsync(instType);
        var retItem = new InstitutionVCM { ItemList = items, InstitutionId = selectedId };
        return View(retItem);
    }

    private async Task<List<NameValueObject>> LoadItemsAsync(int instType)
    {
        try
        {

            var result = await _lookupService.GetInstitutionsByTypeAsync(instType);
            if (result.Failed || result.Value == null)
            {
                return [];
            }
            List<NameValueObject> items = new List<NameValueObject>();
            var list = result.Value.OrderBy(m => m.Name).ToList();
            foreach (var item in list)
            {
                items.Add(new NameValueObject
                {
                    Id = item.InstitutionId,
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
