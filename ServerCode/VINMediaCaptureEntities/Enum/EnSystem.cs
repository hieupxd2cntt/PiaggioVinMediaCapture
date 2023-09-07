using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VINMediaCaptureEntities.Enum;

namespace VINMediaCaptureEntities
{
    public enum EActionLog
    {
        [MappingTo("ADD")]
        [Description("Thêm mới dữ liệu")]
        Add = 1,
        [MappingTo("UPDATE")]
        [Description("Update dữ liệu")]
        Update = 2,
        [MappingTo("DELETE")]
        [Description("Xóa dữ liệu")]
        Delete = 3
    }
   
    public enum EImportType
    {
        User = 1,
        Department = 2,
        Report8966 = 3,
        UnitMoney= 4,
    };
    public enum ETypeCombobox
    {
        Search = 1,
        Edit = 2,
    };
    public enum EStatusUser
    {
        [Description("Sử dụng")]
        Use = 1,
        [Description("Ngừng sử dụng")]
        Stop = 2,
        [Description("Đã xóa")]
        Delete = 4
    };
    public enum EFunctionType
    {
        None = -1,
        Search = 0,
        Insert = 1,
        Update = 2,
        Delete = 3,
        Export = 4,
        Import = 5,
        Cancel = 6,
        Close = 7,
        Save = 8,
        Send = 9,
        Approve = 10,
        Reject = 11,
        General = 12,
        List = 13,
        Word = 14,
        ViewReport = 15,
        PrintScreen = 16
    }
  
  
    public enum EMessageType
    {
        /// <summary>
        /// Message lỗi
        /// </summary>
        ERROR = 0,
        /// <summary>
        /// Message cảnh báo
        /// </summary>
        WARNING = 1,
        /// <summary>
        /// Message thông báo
        /// </summary>
        INFORMATION = 2,
        /// <summary>
        /// Message confirm
        /// </summary>
        CONFIRM = 3,

        CONFIRM_CANCEL = 4
    }
    public enum ESex
    {
        [MappingTo("Nam")]
        [Description("Nam")]
        Male = 1,
        [MappingTo("Nu")]
        [Description("Nữ")]
        FeMale = 2,
        [MappingTo("Other")]
        [Description("Khác")]
        None = 3
    };

    public enum ETagCellGrid
    {
        Normal = 1,
        ReadOnly = 2
    }   
    
    public enum ETypeColorGrid
    {
        User = 1,
        Report = 2
    }

    public enum EStatusMeseager
    {
        [MappingTo("Error")]
        [Description("Lỗi")]
        Error = 0,
        [MappingTo("Success")]
        [Description("Thành công")]
        Success = 1,
    }
    public enum EStatusUnitMoney
    {
        [Description("Tạm ngưng")]
        InActive = 0,   
        [Description("Sử dụng")]
        Active = 1,
        [Description("Đã xóa")]
        Del = 2,
    }
}
