using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Abstract
{
    public interface IPageDal: IRepository<Page>
    {
        Page GetOne();
        List<Page> GetAll(int page, int pageSize);
        int GetAllCount();
    }
}
