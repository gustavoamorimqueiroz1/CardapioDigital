using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Enums;
using CompreAqui.Domain.Models.Settings;
using Microsoft.Extensions.Options;
using System;

namespace CompreAqui.Domain.Attributes
{
    public sealed class AppSettingsService : IAppSettings
    {
        private readonly EnvironmentSettings _environmentSettings;
        private readonly ConnectionBd _connectionBd;
        private readonly JwtSetting _jwtSetting;
        private readonly ConnectionStringSettings _stringConnection;


        public AppSettingsService(IOptions<ConnectionBd> connection, IOptions<EnvironmentSettings> environmentSettings, IOptions<JwtSetting> jwtOptions, IOptions<ConnectionStringSettings> stringConnection)
        {
            ConnectionBd connectionBd = connection?.Value;
            if (connectionBd == null)
                throw new ArgumentException(nameof(connectionBd));
            _connectionBd = connection?.Value;

            EnvironmentSettings environmentSetting1 = environmentSettings?.Value;
            if (environmentSetting1 == null)
                throw new ArgumentException(nameof(environmentSetting1));
            _environmentSettings = environmentSettings?.Value;

            if (jwtOptions == null)
                throw new ArgumentException(nameof(jwtOptions));
            _jwtSetting = jwtOptions?.Value;

            ConnectionStringSettings stringCon = stringConnection?.Value;
            if (stringCon == null)
                throw new ArgumentException(nameof(stringCon));
            _stringConnection = stringConnection?.Value;
        }

        public string GetConnectionString()
        {
            return string.IsNullOrWhiteSpace(_connectionBd.Server) || string.IsNullOrWhiteSpace(_connectionBd.Database) || string.IsNullOrWhiteSpace(_connectionBd.User) || string.IsNullOrWhiteSpace(_connectionBd.Password) || string.IsNullOrWhiteSpace(_connectionBd.Port)
                ? string.Empty
                : $"Server={_connectionBd.Server}; Port={_connectionBd.Port};Database={_connectionBd.Database};UserId={_connectionBd.User};Password={_connectionBd.Password}";
        }

        public string GetStringConnection()
        {
            return string.IsNullOrWhiteSpace(_stringConnection.StringConnection)
                ? string.Empty
                : $"{_stringConnection.StringConnection}";
        }

        public EAppEnvironment GetAppEnvironment()
        {
            return _environmentSettings.Environment;
        }

        public ConnectionBd GetConnectionBd()
        {
            return _connectionBd;
        }

        public ConnectionStringSettings GetConnectionStringSettings()
        {
            return _stringConnection;
        }

        public JwtSetting GetJwtSettings()
        {
            return _jwtSetting;
        }


    }
}
