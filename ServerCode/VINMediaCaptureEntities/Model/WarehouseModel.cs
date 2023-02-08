using VINMediaCaptureEntities.CommonFunction;
using VINMediaCaptureEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VINMediaCaptureEntities.Model
{
    public class WarehouseIndexModel :PagingModel
    {
        public string? DrugName { get; set; }
        private string _StrFromDate { get; set; }
        public string StrFromDate
        {
            get { return _StrFromDate; }
            set
            {
                _StrFromDate = value;
                if (!String.IsNullOrEmpty(_StrFromDate))
                {
                    FromDate = _StrFromDate.StringToDateTime();
                }
                else
                {
                    FromDate = new DateTime(1900, 1, 1);
                }
            }
        }
        private string _StrToDate { get; set; }
        public string StrToDate
        {
            get { return _StrToDate; }
            set
            {
                _StrToDate = value;
                if (!String.IsNullOrEmpty(_StrToDate))
                {
                    ToDate = _StrToDate.StringToDateTime();
                }
                else
                {
                    ToDate = new DateTime(2900, 1, 1);
                }
            }
        }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<WarehouseInfo> WarehouseInfos { get; set; }
        public int? FromQuantity { get; set; }
        public int? ToQuantity { get; set; }
        public WarehouseIndexModel()
        {
            WarehouseInfos = new List<WarehouseInfo>(); 
        }
    }
    public class WarehouseInfo
    {
        public string UnitName { get; set; }
        public string WarehouseUnitName { get; set; }
        public Warehouse Warehouse { get; set; }
        public Drugs Drug { get; set; }
        public WarehouseInfo()
        {
            Warehouse = new Warehouse();
            Drug=new Drugs();
        }
    }
}
