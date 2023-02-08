using VINMediaCaptureEntities.CommonFunction;
using VINMediaCaptureEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction = VINMediaCaptureEntities.Entities.Transaction;

namespace VINMediaCaptureEntities.Model
{
    public class TransactionIndexModel : PagingModel
    {
        public string? TranCode { get; set; }
        public int TranType { get; set; }
        public string? Phone { get; set; }
        public string? Customer { get; set; }
        private string? _StrFromDate { get; set; }
        public string? StrFromDate
        {
            get { return _StrFromDate; }
            set
            {
                _StrFromDate = value;
                if (!String.IsNullOrEmpty(_StrFromDate))
                {
                    FromDate = _StrFromDate.StringToDateTime();
                }
            }
        }
        private string? _StrToDate { get; set; }
        public string? StrToDate
        {
            get { return _StrToDate; }
            set
            {
                _StrToDate = value;
                if (!String.IsNullOrEmpty(_StrToDate))
                {
                    ToDate = _StrToDate.StringToDateTime();
                }
            }
        }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Status { get; set; }
        public List<TransactionInfo> Transactions { get; set; }
        public TransactionIndexModel()
        {
            Transactions = new List<TransactionInfo>();
        }
        public int BranchId { get; set; }
    }
    public class TransactionInfo
    {
        public Transaction Transaction { get; set; }
        public string UserName { get; set; }
    }
    public class TransactionAddModel
    {
        public Transaction Transaction { get; set; }
        public string? CustomerName { get; set; }
        public List<TransactionDetailInfo> TranDetailInf { get; set; }
        public List<AllCode> AllCodes { get; set; }
        public List<DrugInfo> Drugs { get; set; }
        public TransactionAddModel()
        {
            Transaction = new Transaction();
            TranDetailInf = new List<TransactionDetailInfo>();
            Drugs = new List<DrugInfo>();
            AllCodes = new List<AllCode>();
        }
    }
    public class TransactionDetailInfo
    {
        public int TranType { get; set; }
        public TransactionDetail TranDetail { get; set; }
        public string? DrugName { get; set; }
        public string? WareHouseUnitName { get; set; }
        private string? _StringExpireDate { get; set; }
        public string? StringExpireDate
        {
            get { return _StringExpireDate; }
            set
            {
                _StringExpireDate = value;
                if (!String.IsNullOrEmpty(_StringExpireDate))
                {
                    TranDetail.ExpireDate = _StringExpireDate.StringToDateTime();
                }
            }
        }
        public string? UnitName { get; set; }
        public TransactionDetailInfo()
        {
            TranDetail = new TransactionDetail();
        }
    }
    public class DrugInfo
    {
        public Drugs Drug { get; set; }
        public double QuantityAvaiable { get; set; }
        public string UnitName { get; set; }
        public DrugInfo()
        {
            Drug = new Drugs(); 
        }
    }
}
