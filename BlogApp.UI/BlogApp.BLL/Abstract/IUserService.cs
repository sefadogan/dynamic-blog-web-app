using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;

namespace BlogApp.BLL.Abstract
{
    public interface IUserService : IUserDal
    {
        User Login(string email, string password);
    }
}
