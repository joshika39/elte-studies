namespace Bomber.BL.Settings
{
    public interface ISettingsService
    {
        string ConfigurationPath { get; }
        string MapLayoutsPath { get; }
        string DraftMapLayoutsPath { get; }
    }
}
