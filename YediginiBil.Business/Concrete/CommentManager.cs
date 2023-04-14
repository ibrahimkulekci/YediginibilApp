using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Business.Abstract;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public void Create(Comment entity)
        {
            _commentDal.Create(entity);
        }

        public void Delete(Comment entity)
        {
            _commentDal.Delete(entity);
        }

        public List<Comment> GetAll(int page, int pageSize)
        {
            return _commentDal.GetAll(page, pageSize);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _commentDal.GetAll();
        }

        public int GetAllCount()
        {
            return _commentDal.GetAllCount();
        }

        public Comment GetById(int id)
        {
            return _commentDal.GetById(id);
        }

        public void Update(Comment entity)
        {
            _commentDal.Update(entity);
        }
    }
}
