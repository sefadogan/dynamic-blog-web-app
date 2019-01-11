using BlogApp.DAL.UnitOfWork;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.UI.DependencyResolvers.Ninject
{
    public class InterfaceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
        }
    }
}