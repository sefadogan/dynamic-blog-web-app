using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Services
{
    public class UserService : BaseService
    {
        public bool Add(User data)
        {
            try
            {
                Context.Users.Add(data);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(User data)
        {
            try
            {
                Context.Users.Attach(data);
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
                User user = Context.Users.Find(id);
                //Context.Users.Remove(user);
                user.IsActive = false;
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public User BringById(int id)
        {
            return Context.Users.Find(id);
        }
        public List<User> ListAll()
        {
            return Context.Users.ToList();
        }
        public User Login(string email, string password)
        {
            var user = Context.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            return user;
        }
    }
}
