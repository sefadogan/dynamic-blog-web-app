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
    public class RoleManager : IRoleManager
    {
        private readonly IRoleRepository _repository;

        public RoleManager(IRoleRepository repository)
        {
            _repository = repository;
        }

        public bool Add(Role data)
        {
            return _repository.Add(data);
        }
        public Role BringById(int id)
        {
            return _repository.BringById(id);
        }
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
        public List<Role> ListAll()
        {
            return _repository.ListAll();
        }
        public bool Update(Role data)
        {
            return _repository.Update(data);
        }
    }
}
