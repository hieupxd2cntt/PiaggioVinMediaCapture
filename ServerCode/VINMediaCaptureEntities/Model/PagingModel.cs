using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VINMediaCaptureEntities.Model
{
    public abstract class PagingModel
    {
        public int CurrPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
        public PagingModel()
        {
            PageSize = CommonConstant.PageSize;
            CurrPage = 1;
        }
    }
}
