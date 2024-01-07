using Serilog;
using Serilog.Events;

namespace Welisten.Services.Logger.Logger;

public class AppLogger(ILogger logger) : IAppLogger
{
    private readonly string _systemName = "Welisten";
    
    private string ConstructMessage(string messageTemplate, object? module = null)
    {
        if (module == null)
            return $"[{_systemName}] {messageTemplate} ";
        return $"[{_systemName}:{module}] {messageTemplate} ";
    }
    
    public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
    {
        logger?.Write(level, ConstructMessage(messageTemplate), propertyValues);
    }

    public void Write(LogEventLevel level, object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Write(level, ConstructMessage(messageTemplate, module), propertyValues);
    }

    public void Write(LogEventLevel level, Exception exception, string messageTemplate,
        params object[] propertyValues)
    {
        logger?.Write(level, exception, ConstructMessage(messageTemplate), propertyValues);
    }

    public void Write(LogEventLevel level, Exception exception, object module, string messageTemplate,
        params object[] propertyValues)
    {
        logger?.Write(level, exception, ConstructMessage(messageTemplate, module), propertyValues);
    }

    public void Verbose(string messageTemplate, params object[] propertyValues)
    {
        logger?.Verbose(ConstructMessage(messageTemplate), propertyValues);
    }

    public void Verbose(object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Verbose(ConstructMessage(messageTemplate, module), propertyValues);
    }

    public void Debug(string messageTemplate, params object[] propertyValues)
    {
        logger?.Debug(ConstructMessage(messageTemplate), propertyValues);
    }

    public void Debug(object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Debug(ConstructMessage(messageTemplate, module), propertyValues);
    }

    public void Information(string messageTemplate, params object[] propertyValues)
    {
        logger?.Information(ConstructMessage(messageTemplate), propertyValues);
    }

    public void Information(object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Information(ConstructMessage(messageTemplate, module), propertyValues);
    }

    public void Warning(string messageTemplate, params object[] propertyValues)
    {
        logger?.Warning(ConstructMessage(messageTemplate), propertyValues);
    }

    public void Warning(object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Warning(ConstructMessage(messageTemplate, module), propertyValues);
    }

    public void Error(string messageTemplate, params object[] propertyValues)
    {
        logger?.Error(ConstructMessage(messageTemplate), propertyValues);
    }

    public void Error(object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Error(ConstructMessage(messageTemplate, module), propertyValues);
    }

    public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        logger?.Error(exception, ConstructMessage(messageTemplate), propertyValues);
    }

    public void Error(Exception exception, object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Error(exception, ConstructMessage(messageTemplate, module), propertyValues);
    }

    public void Fatal(string messageTemplate, params object[] propertyValues)
    {
        logger?.Fatal(ConstructMessage(messageTemplate), propertyValues);
    }

    public void Fatal(object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Fatal(ConstructMessage(messageTemplate, module), propertyValues);
    }

    public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        logger?.Fatal(exception, ConstructMessage(messageTemplate), propertyValues);
    }

    public void Fatal(Exception exception, object module, string messageTemplate, params object[] propertyValues)
    {
        logger?.Fatal(exception, ConstructMessage(messageTemplate, module), propertyValues);
    }
}