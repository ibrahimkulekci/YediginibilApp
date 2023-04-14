using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface ICommentService
    {
        Comment GetById(int id);
        List<Comment> GetAll(int page, int pageSize);
        IEnumerable<Comment> GetAll();
        void Create(Comment entity);
        void Update(Comment entity);
        void Delete(Comment entity);
        int GetAllCount();
    }
}
