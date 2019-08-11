using BlogApp.BLL.Abstract;
using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BlogApp.BLL.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public User Login(string email, string password)
        {
            var user = Get(x => x.Email == email && x.Password == password);
            return user;
        }
        public bool Add(User user)
        {
            return _userDal.Add(user);
        }
        public bool Delete(int id)
        {
            return _userDal.Delete(id);
        }
        public User Get(Expression<Func<User, bool>> filter)
        {
            return _userDal.Get(filter);
        }
        public List<User> GetList(Expression<Func<User, bool>> filter = null)
        {
            return filter == null
                ? _userDal.GetList()
                : _userDal.GetList(filter);
        }
        public bool Update(User user)
        {
            return _userDal.Update(user);
        }
    }
}
