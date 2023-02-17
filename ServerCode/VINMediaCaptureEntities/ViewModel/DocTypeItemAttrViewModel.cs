using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;

namespace VINMediaCaptureEntities.ViewModel
{
    public class DocTypeItemAttrViewModel
    {
        public DocTypeItemAttr Search { get; set; }
        public DocTypeItems DocTypeItems { get; set; }
        public DocTypeItemAttrViewModel()
        {
            Search = new DocTypeItemAttr();
            DocTypeItems = new DocTypeItems();
        }
    }
}
