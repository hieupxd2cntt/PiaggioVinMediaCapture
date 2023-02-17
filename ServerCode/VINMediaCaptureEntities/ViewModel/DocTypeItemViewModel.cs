using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;

namespace VINMediaCaptureEntities.ViewModel
{
    public class DocTypeItemsViewModel
    {
        public DocTypeItems DocTypeItems { get; set; }
        public DocTypeItemsViewModel()
        {
            DocTypeItems = new DocTypeItems();
        }
    }
}
