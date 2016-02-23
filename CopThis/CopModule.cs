using Autofac;
using CopThis.Data.EntityFramework;
using CopThis.Domain.Entities;

namespace CopThis
{
    internal class CopModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var domainAssembly = typeof(Ticket).Assembly;
            var dataAssembly = typeof(CopContext).Assembly;

            // Register command handlers
            builder.RegisterAssemblyTypes(domainAssembly)
                .Where(t => t.Name.EndsWith("CommandHandler"))
                .AsSelf().AsImplementedInterfaces();

            // Register repositories
            builder.RegisterAssemblyTypes(dataAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            // Register database stuff
            builder.RegisterType<CopContext>()
                .InstancePerRequest()
                .AsSelf().As<IUnitOfWork>();
        }
    }
}