﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Abstract
{
    public interface IBadgeDal:IRepository<Badge>
    {
        Badge GetOne();
        List<Badge> GetAll(int page, int pageSize);
        int GetAllCount();
        IEnumerable<Badge> GetByProductId(int id);
    }
}
