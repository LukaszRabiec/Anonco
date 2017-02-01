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
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            base.Load(builder);
        }
    }
}
