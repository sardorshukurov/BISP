using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Welisten.Common.Extensions;
using Welisten.Services.Settings.AppSettings;

namespace Welisten.API.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public bool OpenApiEnabled => _swaggerSettings.Enabled;

    [BindProperty]
    public string Version => Assembly.GetExecutingAssembly().GetAssemblyVersion()!;


    private readonly SwaggerSettings _swaggerSettings;

    public IndexModel(SwaggerSettings settings)
    {
        _swaggerSettings = settings;
    }

    public void OnGet()
    {

    }
}