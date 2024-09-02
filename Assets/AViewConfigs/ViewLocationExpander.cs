using Microsoft.AspNetCore.Mvc.Razor;

namespace FreeCodeCampAcademy;

public class ViewLocationExpander: IViewLocationExpander
{
    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        return AppViewConfig.CustomSharedDirectories().Union(viewLocations);
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {
        context.Values["customviewlocation"] = nameof(ViewLocationExpander);
    }
}
