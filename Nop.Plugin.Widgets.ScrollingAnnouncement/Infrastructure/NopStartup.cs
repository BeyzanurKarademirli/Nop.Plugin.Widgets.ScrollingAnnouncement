using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Services;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement.Infrastructure
{
    /// <summary>
    /// Servislerimizi nopCommerce Dependency Injection (DI) konteynerine kaydeder.
    /// </summary>
    public class NopStartup : INopStartup
    {
        #region Methods

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Servisimiz DI (Dependency Injection) prensiplerine uygun olarak kaydediliyor
            services.AddScoped<IAnnouncementService, AnnouncementService>();
        }

        public void Configure(IApplicationBuilder application)
        {
        }

        #endregion

        #region Properties

        public int Order => 3000;

        #endregion
    }
}
