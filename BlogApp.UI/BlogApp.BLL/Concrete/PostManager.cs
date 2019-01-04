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
    public class PostManager : IPostManager
    {
        private readonly IPostRepository _repository;

        public PostManager(IPostRepository repository)
        {
            _repository = repository;
        }

        public bool Add(Post data)
        {
            return _repository.Add(data);
        }
        public Post BringById(int id)
        {
            return _repository.BringById(id);
        }
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
        public List<Post> ListAll()
        {
            return _repository.ListAll();
        }
        public bool Update(Post data)
        {
            return _repository.Update(data);
        }
    }
}
