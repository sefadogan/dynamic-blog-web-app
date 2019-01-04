using BlogApp.BLL.Abstract;
using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _repository;

        public UserManager(IUserRepository repository)
        {
            _repository = repository;
        }

        public bool Add(User data)
        {
            return _repository.Add(data);
        }
        public User BringById(int id)
        {
            return _repository.BringById(id);
        }
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
        public List<User> ListAll()
        {
            return _repository.ListAll();
        }
        public bool Update(User data)
        {
            return _repository.Update(data);
        }
    }
}
