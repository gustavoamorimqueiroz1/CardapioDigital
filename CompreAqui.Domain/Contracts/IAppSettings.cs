using CompreAqui.Domain.Enums;
using CompreAqui.Domain.Models.Settings;

namespace CompreAqui.Domain.Contracts
{
    public interface IAppSettings
    {
        string GetConnectionString();
        string GetStringConnection();
        EAppEnvironment GetAppEnvironment();
        ConnectionBd GetConnectionBd();
        ConnectionStringSettings GetConnectionStringSettings();
        JwtSetting GetJwtSettings();
    }
}
