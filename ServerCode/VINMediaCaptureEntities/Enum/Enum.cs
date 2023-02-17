using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VINMediaCaptureEntities.Enum
{
    public enum ESession
    {
        User=1,
        Notification=2,
    }
    public enum EAllCode
    {
        Unit = 1,
        Notification=2
    }
   
    public enum EStatus
    {
        [Description("Đang sử dụng")]
        Active = 0,
        [Description("Ngừng sử dụng")]
        InActive = 1,
    }    
}
