namespace Anonco.Database.IoC
{
    using Autofac;

    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>();
            base.Load(builder);
        }
    }
}
