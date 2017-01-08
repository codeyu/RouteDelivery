using Autofac;
using RouteDelivery.Data;
using RouteDelivery.Data.Implementations;
using RouteDelivery.OptimizationEngine;

namespace RouteDelivery.Web
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.Register(c => new OptimizationEngine.OptimizationEngine(c.Resolve<IUnitOfWork>()))
                .As<IOptimizationEngine>()
                .InstancePerLifetimeScope();
        }
    }
}