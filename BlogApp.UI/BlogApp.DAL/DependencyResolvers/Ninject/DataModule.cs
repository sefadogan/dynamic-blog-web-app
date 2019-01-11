using BlogApp.DAL.Entity;
using BlogApp.DAL.UnitOfWork;
using Ninject.Modules;
using System.Data.Entity;

namespace BlogApp.DAL.DependencyResolvers.Ninject
{
    public class DataModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork.UnitOfWork>().InSingletonScope();
            Bind<DbContext>().To<BlogAppEntities>().InSingletonScope();
            
        }
    }
}
