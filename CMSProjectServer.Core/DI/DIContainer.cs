using Autofac;
using CMSProjectServer.Core.Services;
using CMSProjectServer.DAL;
using Microsoft.Extensions.Configuration;

namespace CMSProjectServer.Core.DI;

public sealed class DIContainer
{
    public static void BuildContainer(ContainerBuilder builder, IConfiguration configuration) => builder.RegisterModule(new CoreModule(configuration));

    private class CoreModule : Module
    {
        private readonly IConfiguration configuration;

        public CoreModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<AuthService>()
                .As<IAuthService>()
                .InstancePerDependency();
            builder.RegisterType<CMSDbContext>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ArticleService>()
                .As<IArticleService>()
                .InstancePerDependency();
            builder.RegisterType<CategoriesService>()
                .As<ICategoriesService>()
                .InstancePerDependency();
            builder.RegisterType<SiteService>()
                .As<ISiteService>()
                .InstancePerDependency();
            builder.RegisterType<LikeService>()
                .As<ILikeService>()
                .InstancePerDependency();
        }
    }
}