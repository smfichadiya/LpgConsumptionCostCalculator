﻿using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using LpgConsumptionCostCalculator.Data.Services;
using System.Web.Http;
using System.Web.Mvc;

namespace LpgConsumptionCostCalculator.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<InMemoryCarData>()
                .As<ICarData>()
                .InstancePerRequest();
            builder.RegisterType<InMemoryFuelReceiptData>()
                .As<IFuelReceiptData>()
                .InstancePerRequest();
            builder.RegisterType<InMemoryCarData>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}