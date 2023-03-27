using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Model;

namespace VINMediaCaptureEntities.ViewModel
{
    public class ProductDocViewModel:PagingModel
    {
        public ProductDoc Search { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public List<ProductDocInfo> ProductDocInfos { get; set; }
        public ProductDocViewModel()
        {
            Search = new ProductDoc();
            ProductDocInfos = new List<ProductDocInfo>();
        }
    }

    public class ProductDocInfo
    {
        public ProductDoc ProductDoc { get; set; }
        public ProductDocVal ProductDocVal { get; set; }
        public DocTypeItems DocTypeItems { get; set; }
        public Entities.Model Model { get; set; }
        public DocType DocType { get; set; }
        public Color Color { get; set; }
        public Market Market { get; set; }
        public Users User { get; set; }
        public ProductDocInfo()
        {
            ProductDoc = new ProductDoc();
            ProductDocVal = new ProductDocVal();
            DocTypeItems = new DocTypeItems();
            DocType = new DocType();
            Model = new Entities.Model();
            Color = new Color();
            Market = new Market();
            User=new Users();
        }
    }

    public class DetailProductDocModel
    {
        public List<ProductDocValInfo> ProductDocValInfo { get; set; }
        public DetailProductDocModel()
        {
            ProductDocValInfo = new List<ProductDocValInfo>();
        }
    }
    public class ProductDocValInfo
    {
        public ProductDocVal ProductDocVal { get; set; }
        public DocTypeItems DocTypeItem { get; set; }
        public DocTypeItemAttr DocTypeItemAttr { get; set; }
        public Users User { get; set; }
        public ProductDocValInfo()
        {
            ProductDocVal = new ProductDocVal();
            DocTypeItem = new DocTypeItems();
            DocTypeItemAttr = new DocTypeItemAttr();
            User = new Users();
        }
    }
}
