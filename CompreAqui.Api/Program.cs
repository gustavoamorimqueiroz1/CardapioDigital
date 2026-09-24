using CompreAqui.Domain.Handlers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;

namespace CompreAqui.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

        private static int FreePort()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 0);
            tcpListener.Start();
            int port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
            tcpListener.Stop();
            return port;
        }

        /// <summary>
        /// Cria o ambiente para se trabalhar
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            int port = FreePort();
            int port_nosecurity = 2000;
            int port_security = 2001;

            return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls($"http://*:{port_nosecurity}", $"https://*:{port_security}")

                //.UseKestrel(options =>
                //{
                //    options.ConfigureHttpsDefaults(listenOptions =>
                //    {
                //        listenOptions.SslProtocols = SslProtocols.Tls12;
                //    });
                //    options.ListenAnyIP(port_nosecurity);
                //    options.ListenAnyIP(port_security, listenOptions =>
                //    {
                //        listenOptions.UseHttps();
                //    });
                //})
                .UseIIS()
                .UseContentRoot(Directory.GetCurrentDirectory());

            });
        }
        
    }
}
