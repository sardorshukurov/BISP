namespace Welisten.Services.Settings.AppSettings;

public class SwaggerSettings
{
    public bool Enabled { get; private set; } = false;

    public string OAuthClientId { get; private set; } = string.Empty;
    public string OAuthClientSecret { get; private set; } = string.Empty;

    public SwaggerSettings()
    {
        Enabled = false;
    }
}