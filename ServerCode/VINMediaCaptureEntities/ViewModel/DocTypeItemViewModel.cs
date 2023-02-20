using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;

namespace VINMediaCaptureEntities.ViewModel
{
    public class ViewBagDropDownModelModel
    {
        
        public List<Entities.Model> Models { get; set; }
        public List<DocType> DocTypes { get; set; }
        public List<Color> Colors { get; set; }
        public List<Market> Markets { get; set; }
        public ViewBagDropDownModelModel()
        {
            Models = new List<Entities.Model>();
            Colors = new List<Color>();
            Markets = new List<Market>();
            DocTypes = new List<DocType>();
        }
    }
    public class DocTypeItemsViewModel
    {
        public DocTypeItems Search { get; set; }
        public List<DocTypeItemsInfo> DataDocTypeItems { get; set; }
        public DocTypeItemsViewModel()
        {
            Search = new DocTypeItems();
            DataDocTypeItems = new List<DocTypeItemsInfo>();
        }
    }
    public class DocTypeItemsInfo
    {
        public DocTypeItems DocTypeItems { get; set; }
        public VINMediaCaptureEntities.Entities.Model Model { get; set; }
        public Color Color { get; set; }
        public Market Market { get; set; }
        public DocTypeItemsInfo()
        {
            DocTypeItems = new DocTypeItems();
            Model = new Entities.Model();
            Color = new Color();
            Market = new Market();

        }
    }
}
