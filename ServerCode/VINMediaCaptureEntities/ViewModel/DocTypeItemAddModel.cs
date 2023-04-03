using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;

namespace VINMediaCaptureEntities.ViewModel
{
    public class DocTypeItemAddModel
    {
        public DocTypeItems DocTypeItems { get; set; }
        //public Entities.Model Model { get; set; }
        public DocTypeItemAttr DocTypeItemAttr { get; set; }
        public DocTypeItemAddModel()
        {
            //Model = new Entities.Model();
            DocTypeItems = new DocTypeItems();
            DocTypeItemAttr = new DocTypeItemAttr();
        }
    }
    
}
