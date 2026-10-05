using CompreAqui.Domain.Attributes;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Models.Settings;
using CompreAqui.Infra.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace CompreAqui.Api.Configuration
{
    public static class StartupConfiguration
    {

        /// <summary>
        /// Metodo extensão de IServiceCollection com a finalidade de definir os parametros para configuração JWT
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="conf">IConfiguration</param>
        public static void AddJwtCompreAqui(this IServiceCollection services, IConfiguration conf)
        {
            var jwtSectionsConfig = conf.GetSection("JwtSettings");
            services.Configure<JwtSetting>(jwtSectionsConfig);
            var jwtSettings = jwtSectionsConfig.Get<JwtSetting>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var conn = conf.GetSection("ConnectionsBd");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.ValidoEm,
                    ValidIssuer = jwtSettings.Emissor
                };
            });           
        }

        public static void AddFacebookAuth(this IServiceCollection services, IConfiguration conf)
        {
            var facebookConfig = conf.GetSection("FacebookAuthSettings");
            services.Configure<FacebookAuthSettings>(facebookConfig);
        }

        public static void AddGlobalVariables(this IServiceCollection services, IConfiguration conf)
        {
            var globalVariablesConfig = conf.GetSection("GlobalVariables");
            services.Configure<GlobalVariables>(globalVariablesConfig);
        }



        /// <summary>
        /// Metodo extensão IServiceCollection Adiciona as dependencias necessárias
        /// </summary>
        /// <param name="services">IServiceCollection da Aplicação</param>
        /// <param name="conf">IConfiguration</param>
        public static void AddDependencies(this IServiceCollection services, IConfiguration conf)
        {
            //services.Configure<ConnectionBd>(conf.GetSection("ConnectionsBd"));
            services.Configure<ConnectionStringSettings>(conf.GetSection("ConnectionString"));
            services.AddSingleton<IAppSettings, AppSettingsService>();
            services.AddTransient<IContext, DataContext>();
            services.RegisterAllTypes<IHandler>(new[] { typeof(IHandler).Assembly }, ServiceLifetime.Scoped);
            var emailConfig = conf.GetSection("EmailSettings").Get<EmailSettings>();
            services.AddSingleton(emailConfig);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AuthenticatedUser>();
        }

        /// <summary>
        /// Metodo extensão IServiceCollection Registra todos dependências do tipo T
        /// </summary>
        /// <typeparam name="T">Tipo a ser instanciado</typeparam>
        /// <param name="services">Extensão de IServiceCollection</param>
        /// <param name="assemblies">instancia da interface para conseguir pegar o executável/dll que contenham todas as classes</param>
        /// <param name="lifetime">tempo de vida - Scoped, Singleton ou Transient</param>
        private static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (TypeInfo type in typesFromAssemblies)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(type);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(type);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(type);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
                }
                //services?.Add(item: new ServiceDescriptor(typeof(T), implementationType: type, lifetime: lifetime));
            }
        }

        /// <summary>
        /// Metodo extensão IServiceCollection Adiciona configuração Swagger
        /// </summary>
        /// <param name="services">IServiceCollection da Aplicação</param>
        public static void AddSwaggerCompreAqui(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                conf =>
                {
                    conf.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = "Compre Aqui System",
                            Version = "V1",
                            Description = "API .net Core 3.1, criada com a finalidade de prover dados aos sistemas de supermercados.",
                            Contact = new OpenApiContact
                            {
                                Name = "Victor Oliveira",
                                Email = "victor-vdo@bol.com.br",
                                Url = new Uri("https://www.facebook.com/victor.vdo")
                            },
                            License = new OpenApiLicense
                            {
                                Name = "Licença de uso",
                                Url = new Uri("https://www.facebook.com/victor.vdo")
                            }
                        });

                    // Cria um arquivo XML contendo os comentários na documentação do Swagger
                    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    conf.IncludeXmlComments(xmlPath);
                });
        }

        /// <summary>
        /// Metodo extensão IApplicationBuilder Registra o Swagger no app
        /// </summary>
        /// <param name="app">App em execução</param>
        public static void RegisterMiddlewareSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(conf =>
            {
                conf.SwaggerEndpoint("/swagger/v1/swagger.json", "CompreAqui.API");
            });
        }
    }
}
