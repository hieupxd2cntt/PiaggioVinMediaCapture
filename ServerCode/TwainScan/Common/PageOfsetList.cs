using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace TwainScan.Common
{
    public class PageOffsetList : IListSource
    {
        public bool ContainsListCollection
        {
            get;
            protected set;
        }
        private long TotalRecords = 0;
        private int pageSize = 0;
        public PageOffsetList(int pageSize, long total)
        {
            TotalRecords = total;
            this.pageSize = pageSize;
        }
        public IList GetList()
        {
            try
            {
                // Return a list of page offsets based on "totalRecords" and "pageSize"   
                var pageOffsets = new List<int>();
                for (int offset = 1; offset <= TotalRecords; offset = offset + pageSize)
                {
                    pageOffsets.Add(offset);
                }
                return pageOffsets;
            }
            catch (Exception ex)
            {
                return new List<int>();
            }

        }
    }
}
