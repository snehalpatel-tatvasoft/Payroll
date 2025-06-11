using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Helper.Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.Helper
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddTransient<ILog4net, Log4net.Log4net>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
