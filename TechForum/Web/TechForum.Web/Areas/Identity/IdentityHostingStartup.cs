using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TechForum.Web.Areas.Identity.IdentityHostingStartup))]

namespace TechForum.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
