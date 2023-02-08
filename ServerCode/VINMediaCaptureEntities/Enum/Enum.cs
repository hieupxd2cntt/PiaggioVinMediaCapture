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
    public enum ENotification
    {
        [Description("Số lượng sắp hết")]
        Quantity = 0,
        [Description("Số ngày sắp hết hạn cần cảnh bảo")]
        ExpireDate = 1,
    }
    public enum EStatus
    {
        [Description("Đang sử dụng")]
        Active = 0,
        [Description("Ngừng sử dụng")]
        InActive = 1,
    }
    public enum ETranStatus
    {
        [Description("Mới")]
        New = 0,
        [Description("Đã duyệt")]
        Approve = 1,
        [Description("Đã hủy")]
        Cancel = 2,
        [Description("Đã hủy")]
        Reject = 3,
    }
    public enum ETransactionType
    {
        [Description("Nhập hàng")]
        [MappingTo("NhapHang")]
        Input = 1,
        [Description("Xuất hàng")]
        [MappingTo("XuatHang")]
        Output = 2,
        [Description("Luân chuyển")]
        [MappingTo("LuanChuyen")]
        Transfer =3,
        [Description("Hủy")]
        [MappingTo("Huy")]
        Destroy =4,
        [Description("Trả nhà cung cấp")]
        [MappingTo("TraNhaCungCap")]
        Return =5 ,
    }
    public enum EUserInfoSendToApi
    {
        CurrentUserID = 1,
        CurrentUserName = 2,
        CurrentBranchId = 3,
        CurrentBranchName = 4,
    }
    public enum EWarehouseStatus
    {
        [Description("Mới")]
        New = 0,
        [Description("Đang bán")]
        Active = 1,
        [Description("Dừng bán")]
        Stop = 2,
        [Description("Đã hủy")]
        Cancel = 3,
    }
    
}
