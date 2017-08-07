namespace SampleApp.Common.Configuration
{
    public interface IAppConfig
    {
        T GetConfigurationValue<T>(string key);
        string GetConfigurationValue(string key);
        T GetConfigurationValue<T>(string key, T defaultValue);
        string GetConfigurationValue(string key, string defaultValue);
    }

    public interface IAppConfig<out TDto> : IAppConfig
    {
        TDto Options { get; }
    }
}