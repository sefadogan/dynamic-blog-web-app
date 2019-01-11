using BlogApp.DAL.UnitOfWork;
using Ninject.Modules;

namespace BlogApp.BLL.Dependency_Resolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
           // Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
        }
    }
}
