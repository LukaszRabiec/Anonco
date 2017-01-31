namespace Anonco.Logic.IoC
{
    using Autofac;
    using Repositories.Abstract;
    using Repositories.Concrete;

    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AnnouncementRepository>().As<IAnnouncementRepository>();
            base.Load(builder);
        }
    }
}
