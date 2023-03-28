using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Abstract
{
    public interface IProductDal:IRepository<Product>
    {
        //product özel methotları ekle.
        List<Product> GetAll(int page, int pageSize);
        int GetAllCount();

    }
}
