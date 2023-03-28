using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Business.Model
{
    public class BaseResultListModel<TDataListItem>
    {
        public List<TDataListItem> DataList { get; set; }

        public long TotalRecordCount { get; set; }
    }
}
