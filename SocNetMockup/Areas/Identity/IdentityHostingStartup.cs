using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SocNetMockup.Areas.Identity.IdentityHostingStartup))]
namespace SocNetMockup.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}