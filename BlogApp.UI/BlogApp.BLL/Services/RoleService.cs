using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Services
{
    public class RoleService : BaseService
    {
        public bool Add(Role data)
        {
            try
            {
                Context.Roles.Add(data);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(Role data)
        {
            try
            {
                Context.Roles.Attach(data);
                Context.Entry(data).State = EntityState.Modified;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                Role role = Context.Roles.Find(id);
                Context.Roles.Remove(role);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Role BringById(int id)
        {
            return Context.Roles.Find(id);
        }
        public List<Role> ListAll()
        {
            return Context.Roles.ToList();
        }
        public Role BringByEmail(string email)
        {
            var user = Context.Users.FirstOrDefault(x => x.Email == email);
            return user.Role;
        }
    }
}
