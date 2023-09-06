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
        Notification = 2
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
        VARCHAR=1,
        [Description("Số nguyên")]
        INTEGER =2,
        [Description("Số thực")]
        FLOAT =3,
        [Description("Đúng/Sai")]
        BOOLEAN =4,
        [Description("Ngày")]
        DATE =5,
        DATFILE=6,
        [Description("Hình Ảnh")]
        IMGCAPT =7
    }
}
