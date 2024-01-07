using Microsoft.Extensions.Configuration;

namespace Welisten.Common.Settings;

public abstract class CommonSettings
{
    public static T Load<T>(string key, IConfiguration configuration = null)
    {
        var settings = (T)Activator.CreateInstance(typeof(T));

        CommonSettingsFactory.Create(configuration).GetSection(key)
            .Bind(settings, (x) => { x.BindNonPublicProperties = true; });

        return settings;
    }
}