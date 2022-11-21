using ParsKyanCrm.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common.Enums
{
    public enum UserRoleAdminRoles : int
    {
        #region ساختار سیستم
        
        [Display(Name = "مشاهده")] 
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "/Admin/Users/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "کاربران سیستم")]
        [OrderAttribute(Order = 1)]
        Users = 102,

        [Display(Name = "ویرایش و افزودن")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "کاربران سیستم")]
        [OrderAttribute(Order = 1)]
        Users_Save = 103,

        
        
        [Display(Name = "مشاهده و ویرایش و افزودن")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تنظیم سطوح دسترسی")]
        [OrderAttribute(Order = 1)]
        SetAccessLevels = 104,

        
        
        [Display(Name = "مشاهده")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "/Admin/State/index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تعریف استان")]
        [OrderAttribute(Order = 2)]
        State = 105,

        [Display(Name = "ویرایش و افزودن")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تعریف استان")]
        [OrderAttribute(Order = 2)]
        State_Save = 106,

        [Display(Name = "مشاهده")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "/Admin/City/index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تعریف شهر")]
        [OrderAttribute(Order = 2)]
        City = 107,

        [Display(Name = "ویرایش و افزودن")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تعریف شهر")]
        [OrderAttribute(Order = 2)]
        City_Save = 108,

        [Display(Name = "مشاهده")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "/Admin/SystemSeting/index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تنظیمات سیستم")]
        [OrderAttribute(Order = 2)]
        SystemSeting = 109,

        [Display(Name = "ویرایش و افزودن")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تنظیمات سیستم")]
        [OrderAttribute(Order = 2)]
        SystemSeting_Save = 110,

        #endregion

        #region اطلاعات پایه
        [Display(Name = "مشاهده")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "اطلاعات پایه")]
        [DisplayFiledAttribute1(Name = "#")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "ثبت گروه شرکت ها")]
        [OrderAttribute(Order = 1)]
        CompanyGroup = 111,


        [Display(Name = "مشاهده")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "اطلاعات پایه")]
        [DisplayFiledAttribute1(Name = "#")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "نرخ نامه قرارداد")]
        [OrderAttribute(Order = 1)]
        ServiceFee = 112,

        #endregion


        #region امور سایت
        [Display(Name = "مشاهده")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "#")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "مدیریت محتوای سایت")]
        [OrderAttribute(Order = 1)]
        NewsAndContent = 113,


        [Display(Name = "مشاهده")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "#")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "فرصت های شغلی")]
        [OrderAttribute(Order = 1)]
        ServiyceFee = 114,


        [Display(Name = "مشاهده")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "#")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "ثبت امتیازات شرکت ها")]
        [OrderAttribute(Order = 1)]
        RankingOfCompanies = 115,

        #endregion

        #region پشتیبانی
        [Display(Name = "مشاهده")]
        [Category("Support")]
        [DisplayFiledAttribute(Name = "پشتیبانی")]
        [DisplayFiledAttribute1(Name = "#")]
        [DisplayFiledAttribute2(Name = "fa fa-support")]
        [DisplayFiledAttribute3(Name = "تیکت ها")]
        [OrderAttribute(Order = 1)]
        Tickets = 116,       

        #endregion

      
    }
}
