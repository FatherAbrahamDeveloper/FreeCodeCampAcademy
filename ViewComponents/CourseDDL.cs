using FreeCodeCampAcademy.APIService.APIContracts;
using Microsoft.AspNetCore.Mvc;

namespace FreeCodeCampAcademy.ViewComponents;

public class CourseDDL(ILookupService lookup) : ViewComponent
{
    private readonly ILookupService _lookupService = lookup;

    public async Task<IViewComponentResult> InvokeAsync(int facultyId, int selectedId)
    {
        List<NameValueObject> items = await LoadItemsAsync(facultyId);
        var retItem = new CourseVCM { ItemList = items, FacultyId = facultyId, CourseId = selectedId };
        return View(retItem);
    }

    private async Task<List<NameValueObject>> LoadItemsAsync(int facultyId)
    {
        try
        {

            var result = await _lookupService.GetCoursesAsync(facultyId);
            if (result.Failed || result.Value == null)
            {
                return [];
            }
            var list = result.Value.OrderBy(m => m.Name).ToList();
            List<NameValueObject> items = [];
            foreach (var item in list)
            {
                items.Add(new NameValueObject
                {
                    Id = item.CourseOfStudyId,
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

