using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fatca
{
    public class GetListEnum
    {
        //public List<ComboBoxModel> GetListStatus()
        //{
        //    var comboboxs = Enum.GetValues(typeof(EStatus)).Cast<EStatus>().Where(x=>x!=EStatus.Delete)
        //               .Select(x => new ComboBoxModel
        //               {
        //                   Id = (int)x,
        //                   Value = x.GetDescription()
        //               })
        //               .ToList();
        //    return comboboxs;
        //}
        //public List<ComboBoxModel> GetListStatusDepartment()
        //{
        //    var comboboxs = Enum.GetValues(typeof(EStatusDepartment)).Cast<EStatusDepartment>().Where(x => x != EStatusDepartment.Delete)

        //               .Select(x => new ComboBoxModel
        //               {
        //                   Id = (int)x,
        //                   Value = x.GetDescription()
        //               })
        //               .ToList();
        //    return comboboxs;
        //}
        //public List<ComboBoxModel> GetListStatusDepartmentSendSBV()
        //{
        //    var comboboxs = Enum.GetValues(typeof(EStatusDepartmentSendSBV)).Cast<EStatusDepartmentSendSBV>().Where(x => (int)x != (int)EStatusDepartmentSendSBV.Sent)

        //               .Select(x => new ComboBoxModel
        //               {
        //                   Id = (int)x,
        //                   Value = x.GetDescription()
        //               })
        //               .ToList();
        //    return comboboxs;
        //}
        ///// <summary>
        ///// Lấy list trạng thái của báo cáo của SBV
        ///// </summary>
        ///// <returns></returns>
        //public List<ComboBoxModel> GetListStatusSBV()
        //{
        //    var comboboxs = Enum.GetValues(typeof(EStatus8966SBV)).Cast<EStatus8966SBV>()
        //               .Select(x => new ComboBoxModel
        //               {
        //                   Id = (int)x,
        //                   Value = x.GetDescription()
        //               })
        //               .ToList();
        //    return comboboxs;
        //}
        ///// <summary>
        ///// Lấy trạng thái của báo cáo 8966 gửi sang IRS
        ///// </summary>
        ///// <returns></returns>
        //public List<ComboBoxModel> GetListStatusSendIRS()
        //{
        //    var comboboxs = Enum.GetValues(typeof(EStatus8966SendIRS)).Cast<EStatus8966SendIRS>().Where(x => x != EStatus8966SendIRS.Delete && x != EStatus8966SendIRS.General)
                        
        //               .Select(x => new ComboBoxModel
        //               {
        //                   Id = (int)x,
        //                   Value = x.GetDescription()
        //               })
        //               .ToList();
        //    return comboboxs;
        //}
        ///// <summary>
        ///// Lấy list trạng thái của báo cáo của FI
        ///// </summary>
        ///// <returns></returns>
        //public List<ComboBoxModel> GetListStatusFI()
        //{
        //    var comboboxs = Enum.GetValues(typeof(EStatus8966FI)).Cast<EStatus8966FI>().Where(x => x != EStatus8966FI.Delete)
        //               .Select(x => new ComboBoxModel
        //               {
        //                   Id = (int)x,
        //                   Value = x.GetDescription()
        //               })
        //               .ToList();
        //    return comboboxs;
        //}
        ///// <summary>
        ///// Lấy danh sách giới tính 
        ///// </summary>
        ///// <returns></returns>
        //public List<ComboBoxModel> GetListSex()
        //{
        //    var comboboxs = Enum.GetValues(typeof(ESex)).Cast<ESex>()
        //               .Select(x => new ComboBoxModel
        //               {
        //                   Id = (int)x,
        //                   Value = x.GetDescription()
        //               })
        //               .ToList();
        //    return comboboxs;
        //}
        ///// <summary>
        ///// Lấy trạng thái user
        ///// </summary>
        ///// <returns></returns>
        //public List<ComboBoxModel> GetListStatusUser()
        //{
        //    var comboboxs = Enum.GetValues(typeof(EStatusUser)).Cast<EStatusUser>()
        //                .Where(x=>x !=EStatusUser.Delete)
        //               .Select(x => new ComboBoxModel
        //               {
        //                   Id = (int)x,
        //                   Value = x.GetDescription()
        //               })
                       
        //               .ToList();
        //    return comboboxs;
        //}
        //public void InsertComboxModel(List<ComboBoxModel> comboBoxs,int type)
        //{
        //    comboBoxs.Insert(0, new ComboBoxModel { Id = -1, Value =type==(int)ETypeCombobox.Search? "--- Tất cả --- ":"--- Chọn ---"});
        //}
    }
}
