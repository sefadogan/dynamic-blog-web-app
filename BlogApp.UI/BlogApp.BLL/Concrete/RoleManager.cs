using BlogApp.BLL.Abstract;
using BlogApp.DAL.Abstract;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BlogApp.BLL.Concrete
{
    public class RoleManager : IRoleService
    {
        IRoleDal _roleDal;

        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }

        public bool Add(Role role)
        {
            return _roleDal.Add(role);
        }
        public bool Delete(int id)
        {
            return _roleDal.Delete(id);
        }
        public Role Get(Expression<Func<Role, bool>> filter)
        {
            return _roleDal.Get(filter);
        }
        public List<Role> GetList(Expression<Func<Role, bool>> filter = null)
        {
            return filter == null
                ? _roleDal.GetList()
                : _roleDal.GetList(filter);
        }
        public bool Update(Role role)
        {
            return _roleDal.Update(role);
        }
    }
}
