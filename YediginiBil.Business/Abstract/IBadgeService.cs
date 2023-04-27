using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface IBadgeService
    {
        Badge GetById(int id);
        List<Badge> GetAll(int page, int pageSize);
        IEnumerable<Badge> GetAll();
        IEnumerable<Badge> GetByProductId(int id);

        void Create(Badge entity);
        void Update(Badge entity);
        void Delete(Badge entity);
        int GetAllCount();
    }
}
