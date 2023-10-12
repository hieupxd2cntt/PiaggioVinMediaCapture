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
        User = 1,
        Notification = 2,
    }
    public enum EAllCode
    {
        Unit = 1,
        Notification = 2,
        [MappingTo("ScanSetting")]
        ScanSetting = 3,
    }
    public enum EScanSetting
    {
        [Description("Tự động kéo giấy")]
        [MappingTo("UseAdf")]
        UseAdf=1,
        [Description("Scan 2 mặt")]
        [MappingTo("UseDuplex")]
        UseDuplex = 2,
        [Description("Dùng giao diện máy scan")]
        [MappingTo("UseUI")]
        UseUI = 3,
        [Description("Hiển thị hiện trạng")]
        [MappingTo("ShowProgressIndicator")]
        ShowProgressIndicator=4,
        [Description("Quét đen trắng")]
        [MappingTo("BlackAndWhite")]
        BlackAndWhite=5,
        [Description("Chọn vùng quét")]
        [MappingTo("Area")]
        Area = 6, 
        [Description("Tự nhận lề giấy ")]
        [MappingTo("AutoDetectBorder")]
        AutoDetectBorder = 7,
        [Description("Tự đảo chiều ảnh")]
        [MappingTo("AutoRotate")]
        AutoRotate = 8,
        [Description("Gửi pdf")]
        [MappingTo("SendPdf")]
        SendPdf = 8,
        [Description("DPI")]
        [MappingTo("DPI")]
        DPI =9,

    }
    public enum EStatus
    {
        [Description("Đang sử dụng")]
        Active = 0,
        [Description("Ngừng sử dụng")]
        InActive = 1,
    }
    public enum EDocType
    {
        [Description("Thu thập QC")]
        [MappingTo("ThuThapQC")]
        ThuThapQC = 1,
        [Description("Thu thập tài liệu")]
        [MappingTo("ThuThapTaiLieu")]
        ThuThapTaiLieu = 2,
    }
    public enum EAttrDataType
    {
        [Description("Text")]
        VARCHAR = 1,
        [Description("Số nguyên")]
        INTEGER = 2,
        [Description("Số thực")]
        FLOAT = 3,
        [Description("Đúng/Sai")]
        BOOLEAN = 4,
        [Description("Ngày")]
        DATE = 5,
        DATFILE = 6,
        [Description("Hình Ảnh")]
        IMGCAPT = 7
    }
}
