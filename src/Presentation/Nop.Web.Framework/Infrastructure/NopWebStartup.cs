﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Web.Framework.Infrastructure.Extensions;

namespace Nop.Web.Framework.Infrastructure
{
    /// <summary>
    /// Represents object for the configuring web services and middleware on application startup
    /// </summary>
    public class NopWebStartup : IStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            //add HTTP sesion state feature
            services.AddHttpSession();

            //add localization
            services.AddLocalization();

            //add and configure MVC feature
            services.AddNopMvc();

            //add MiniProfiler services
            services.AddMiniProfiler();
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            //use request localization
            application.UseRequestLocalization();

            //set request culture
            application.UseCulture();

            //use HTTP session
            application.UseSession();

            //MVC routing
            application.UseNopMvc();

            //add MiniProfiler
            application.UseMiniProfiler();
        }

        /// <summary>
        /// Gets order of this startup configuration implementation (the lower the better) 
        /// </summary>
        public int Order
        {
            //web services should be loaded last
            get { return 1000; }
        }
    }
}