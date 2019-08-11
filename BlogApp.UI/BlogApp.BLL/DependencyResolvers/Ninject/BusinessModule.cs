using BlogApp.BLL.Abstract;
using BlogApp.BLL.Concrete;
using BlogApp.DAL.Abstract;
using BlogApp.DAL.Concrete.EntityFramework;
using Ninject.Modules;

namespace BlogApp.BLL.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryManager>();
            Bind<ICommentService>().To<CommentManager>();
            Bind<IPostService>().To<PostManager>();
            Bind<IRoleService>().To<RoleManager>();
            Bind<IUserService>().To<UserManager>();

            Bind<ICategoryDal>().To<EfCategoryDal>();
            Bind<ICommentDal>().To<EfCommentDal>();
            Bind<IPostDal>().To<EfPostDal>();
            Bind<IRoleDal>().To<EfRoleDal>();
            Bind<IUserDal>().To<EfUserDal>();
        }
    }
}
