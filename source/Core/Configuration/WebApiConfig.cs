/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license
 */
using System;
using System.IO;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Thinktecture.IdentityManager
{
    class WebApiConfig
    {
        public static void Configure(HttpConfiguration httpConfiguration)
        {
            if (httpConfiguration == null) throw new ArgumentNullException("httpConfiguration");

            httpConfiguration.MapHttpAttributeRoutes();

            httpConfiguration.SuppressDefaultHostAuthentication();
            httpConfiguration.Filters.Add(new HostAuthenticationAttribute("Bearer"));
            //apiConfig.Filters.Add(new AuthorizeAttribute(){Roles=config.AdminRoleName});

            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.XmlFormatter);
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            //apiConfig.Services.Add(typeof(IExceptionLogger), new UserAdminExceptionLogger());
        }

        public class UserAdminExceptionLogger : ExceptionLogger
        {
            public override void Log(ExceptionLoggerContext context)
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
                path = Path.Combine(path, "UserAdminException.txt");
                Directory.CreateDirectory(path);
                var msg = DateTime.Now.ToString() + Environment.NewLine + context.Exception.ToString() + Environment.NewLine + Environment.NewLine;
                File.AppendAllText(path, msg);
            }
        }
    }
}
