namespace Welisten.Services.Settings.AppSettings;

public class MainSettings
{
    public string PublicUrl { get; private set; } = string.Empty;
    public string InternalUrl { get; private set; } = string.Empty;
    public string AllowedOrigins { get; private set; } = string.Empty;
    public int UploadFileSizeLimit { get; private set; } = 20971520;
}