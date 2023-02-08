using VINMediaCaptureEntities.CommonFunction;
using VINMediaCaptureEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VINMediaCaptureEntities.Model
{
    public class NotificationModel
    {
        public List<DrugInfo> DrugQuantityNotification { get; set; }
        public List<DrugInfo> DrugExpireNotification { get; set; }
        public NotificationModel()
        {
            DrugQuantityNotification = new List<DrugInfo>();
            DrugExpireNotification = new List<DrugInfo>();
        }
    }    
}
