/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license
 */
using Autofac;
using Autofac.Integration.WebApi;
using System;
using DotNetDoodle.Owin.Dependencies.Autofac;
using Thinktecture.IdentityManager.Core;

namespace Thinktecture.IdentityManager
{
    internal class AutofacConfig
    {
        public static IContainer Configure(IdentityManagerConfiguration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof(AutofacConfig).Assembly);
            builder.RegisterOwinApplicationContainer();

            builder.Register(ctx => config.UserManagerFactory())
                .As<IUserManager>()
                .InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}
