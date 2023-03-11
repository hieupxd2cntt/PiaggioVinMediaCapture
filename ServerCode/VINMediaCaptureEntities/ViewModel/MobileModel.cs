using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;

namespace VINMediaCaptureEntities.Model
{
    public class MobileDoctypeGuideInsertModel
    {
        public string CurrentSession { get; set; }
        public DocTypeGuide DocTypeGuide { get; set; }
        public MobileDoctypeGuideInsertModel()
        {
            DocTypeGuide = new DocTypeGuide();
        }

    }
}
