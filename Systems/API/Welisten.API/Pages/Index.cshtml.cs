using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Welisten.Common.Extensions;
using Welisten.Services.Settings.AppSettings;

namespace Welisten.API.Pages;

public class IndexModel(SwaggerSettings settings) : PageModel
{
    [BindProperty]
    public bool OpenApiEnabled => settings.Enabled;

    [BindProperty]
    public string Version => Assembly.GetExecutingAssembly().GetAssemblyVersion()!;


    public void OnGet()
    {

    }
}