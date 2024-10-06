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

        [Display(Name = "حذف")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "کاربران سیستم")]
        [OrderAttribute(Order = 1)]
        Users_Delete = 301,

        [Display(Name = "تغییر کلمه عبور")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "کاربران سیستم")]
        [OrderAttribute(Order = 1)]
        Users_UpdatePass = 893,

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
        [OrderAttribute(Order = 1)]
        State = 105,

        [Display(Name = "ویرایش و افزودن")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تعریف استان")]
        [OrderAttribute(Order = 1)]
        State_Save = 106,

        [Display(Name = "مشاهده")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "/Admin/City/index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تعریف شهر")]
        [OrderAttribute(Order = 1)]
        City = 107,

        [Display(Name = "ویرایش و افزودن")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تعریف شهر")]
        [OrderAttribute(Order = 1)]
        City_Save = 108,

        [Display(Name = "مشاهده")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "/Admin/SystemSeting/index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تنظیمات سیستم")]
        [OrderAttribute(Order = 1)]
        SystemSeting = 109,

        [Display(Name = "ویرایش و افزودن")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تنظیمات سیستم")]
        [OrderAttribute(Order = 1)]
        SystemSeting_Save = 110,

        [Display(Name = "مشاهده")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "/Admin/Customers/index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "مشتریان")]
        [OrderAttribute(Order = 1)]
        Customers = 833,

        [Display(Name = "ویرایش و افزودن")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "مشتریان")]
        [OrderAttribute(Order = 1)]
        Customers_Save = 834,

        [Display(Name = "حذف")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "مشتریان")]
        [OrderAttribute(Order = 1)]
        Customers_Delete = 835,

        #endregion

        #region اطلاعات پایه

        [Display(Name = "مشاهده")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "اطلاعات پایه")]
        [DisplayFiledAttribute1(Name = "/Admin/Companies/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "شرکت ها")]
        [OrderAttribute(Order = 2)]
        Companies = 111,

        [Display(Name = "ویرایش و افزودن")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "اطلاعات پایه")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "شرکت ها")]
        [OrderAttribute(Order = 2)]
        Companies_Save = 897,

        [Display(Name = "حذف")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "اطلاعات پایه")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "شرکت ها")]
        [OrderAttribute(Order = 2)]
        Companies_Delete = 798,

        [Display(Name = "مشاهده")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "اطلاعات پایه")]
        [DisplayFiledAttribute1(Name = "/Admin/ServiceFee/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "نرخ نامه قرارداد")]
        [OrderAttribute(Order = 2)]
        ServiceFee = 112,

        [Display(Name = "ویرایش و افزودن")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "اطلاعات پایه")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "نرخ نامه قرارداد")]
        [OrderAttribute(Order = 2)]
        ServiceFee_Save = 190,

        [Display(Name = "حذف")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "اطلاعات پایه")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "نرخ نامه قرارداد")]
        [OrderAttribute(Order = 2)]
        ServiceFee_Delete = 901,

        [Display(Name = "مشاهده")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "اطلاعات پایه")]
        [DisplayFiledAttribute1(Name = "/Admin/Contract/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "قرارداد و اصلاحیه قرارداد")]
        [OrderAttribute(Order = 2)]
        Contract = 333,

        [Display(Name = "ویرایش و افزودن")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "اطلاعات پایه")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "قرارداد و اصلاحیه قرارداد")]
        [OrderAttribute(Order = 2)]
        Contract_Save = 334,

        [Display(Name = "حذف")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "اطلاعات پایه")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "قرارداد و اصلاحیه قرارداد")]
        [OrderAttribute(Order = 2)]
        Contract_Delete = 433,

        //[Display(Name = "مشاهده")]
        //[Category("BaseInfo")]
        //[DisplayFiledAttribute(Name = "اطلاعات پایه")]
        //[DisplayFiledAttribute1(Name = "/Admin/LoginLog/Index")]
        //[DisplayFiledAttribute2(Name = "fa fa-th-large")]
        //[DisplayFiledAttribute3(Name = "خطا های ورود به ادمین")]
        //[OrderAttribute(Order = 2)]
        //LoginLog = 917,

        #endregion

        #region امور سایت

        [Display(Name = "مشاهده")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "/Admin/NewsAndContent/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "محتوای سایت")]
        [OrderAttribute(Order = 3)]
        NewsAndContent = 113,

        [Display(Name = "ویرایش و افزودن")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "محتوای سایت")]
        [OrderAttribute(Order = 3)]
        NewsAndContent_Save = 654,

        [Display(Name = "حذف")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "محتوای سایت")]
        [OrderAttribute(Order = 3)]
        NewsAndContent_Delete = 456,

        [Display(Name = "مشاهده")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "/Admin/CareerOpportunities/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "فرصت های شغلی")]
        [OrderAttribute(Order = 3)]
        CareerOpportunities = 114,

        [Display(Name = "ویرایش و افزودن")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "فرصت های شغلی")]
        [OrderAttribute(Order = 3)]
        CareerOpportunities_Save = 767,


        [Display(Name = "مشاهده")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "/Admin/RankingOfCompanies/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "امتیازات شرکت ها")]
        [OrderAttribute(Order = 3)]
        RankingOfCompanies = 115,

        [Display(Name = "ویرایش و افزودن")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "امتیازات شرکت ها")]
        [OrderAttribute(Order = 3)]
        RankingOfCompanies_Save = 760,

        [Display(Name = "حذف")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "امتیازات شرکت ها")]
        [OrderAttribute(Order = 3)]
        RankingOfCompanies_Delete = 607,

        [Display(Name = "مشاهده")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "/Admin/Activity/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "فعالیتهای شرکت")]
        [OrderAttribute(Order = 3)]
        Activity = 179,

        [Display(Name = "ویرایش و افزودن")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "فعالیتهای شرکت")]
        [OrderAttribute(Order = 3)]
        Activity_Save = 189,

        [Display(Name = "حذف")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "فعالیتهای شرکت")]
        [OrderAttribute(Order = 3)]
        Activity_Delete = 981,

        [Display(Name = "مشاهده")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "/Admin/AboutUs/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "درباره شرکت")]
        [OrderAttribute(Order = 3)]
        AboutUs = 876,

        [Display(Name = "مشاهده")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "/Admin/ManagerOfParsKyan/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "ساختار مدیریت")]
        [OrderAttribute(Order = 3)]
        ManagerOfParsKyan = 909,

        [Display(Name = "ویرایش و افزودن")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "ساختار مدیریت")]
        [OrderAttribute(Order = 3)]
        ManagerOfParsKyan_Save = 910,

        [Display(Name = "حذف")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "ساختار مدیریت")]
        [OrderAttribute(Order = 3)]
        ManagerOfParsKyan_Delete = 921,



        [Display(Name = "مشاهده")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "/Admin/LicensesAndHonors/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "جوایز و افتخارات")]
        [OrderAttribute(Order = 3)]
        LicensesAndHonors = 555,

        [Display(Name = "ویرایش و افزودن")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "جوایز و افتخارات")]
        [OrderAttribute(Order = 3)]
        LicensesAndHonors_Save = 556,

        [Display(Name = "حذف")]
        [Category("Landing")]
        [DisplayFiledAttribute(Name = "امور سایت")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "جوایز و افتخارات")]
        [OrderAttribute(Order = 3)]
        LicensesAndHonors_Delete = 655,


        #endregion

        #region پشتیبانی

        [Display(Name = "مشاهده")]
        [Category("Support")]
        [DisplayFiledAttribute(Name = "پشتیبانی")]
        [DisplayFiledAttribute1(Name = "/Admin/Tickets/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-support")]
        [DisplayFiledAttribute3(Name = "تیکت ها")]
        [OrderAttribute(Order = 4)]
        Tickets = 116,

        #endregion

        #region درخواست ها و گزارش ها

        [Display(Name = "مشاهده")]
        [Category("Report")]
        [DisplayFiledAttribute(Name = "درخواست ها و گزارش ها")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "درخواست ها و گزارشات کلی")]
        [OrderAttribute(Order = 4)]
        AllReportVisitor = 547,

        [Display(Name = "مشاهده")]
        [Category("Report")]
        [DisplayFiledAttribute(Name = "درخواست ها و گزارش ها")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "درخواست ها و گزارشات وزارت سمت")]
        [OrderAttribute(Order = 4)]
        SamtReportVisitor = 548,

        [Display(Name = "مشاهده")]
        [Category("Report")]
        [DisplayFiledAttribute(Name = "درخواست ها و گزارش ها")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "درخواست ها و گزارشات حاکمیت شرکتی")]
        [OrderAttribute(Order = 4)]
        CopReportVisitor = 549,
        #endregion

        #region حاکمیت شرکتی

        [Display(Name = "مشاهده")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "/Admin/Corporate/DataForm")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "دسته سوالات")]
        [OrderAttribute(Order = 5)]
        CorporateDataForm = 120,

        [Display(Name = "ویرایش و افزودن")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "دسته سوالات")]
        [OrderAttribute(Order = 5)]
        CorporateDataForm_Save = 900,

        [Display(Name = "حذف")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "دسته سوالات")]
        [OrderAttribute(Order = 5)]
        CorporateDataForm_Delete = 800,

        [Display(Name = "مشاهده")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "/Admin/Corporate/DataFormQuestions")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "سوالات")]
        [OrderAttribute(Order = 5)]
        CorporateDataFormQuestions = 121,

        [Display(Name = "ویرایش و افزودن")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "سوالات")]
        [OrderAttribute(Order = 5)]
        CorporateDataFormQuestions_Save = 902,

        [Display(Name = "حذف")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "سوالات")]
        [OrderAttribute(Order = 5)]
        CorporateDataFormQuestions_Delete = 801,

        [Display(Name = "مشاهده")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "/Admin/Corporate/DataFormQuestionsOptione")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "گزینه های لیکرتی")]
        [OrderAttribute(Order = 5)]
        CorporateDataFormQuestionsOptione = 122,

        [Display(Name = "ویرایش و افزودن")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "گزینه های لیکرتی")]
        [OrderAttribute(Order = 5)]
        CorporateDataFormQuestionsOptione_Save = 903,

        [Display(Name = "حذف")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "گزینه های لیکرتی")]
        [OrderAttribute(Order = 5)]
        CorporateDataFormQuestionsOptione_Delete = 802,

        [Display(Name = "مشاهده")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "/Admin/Corporate/DataFormDocument")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "مدارک مورد نیاز")]
        [OrderAttribute(Order = 5)]
        CorporateDataFormDocument = 123,

        [Display(Name = "ویرایش و افزودن")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "مدارک مورد نیاز")]
        [OrderAttribute(Order = 5)]
        CorporateDataFormDocument_Save = 904,

        [Display(Name = "حذف")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "مدارک مورد نیاز")]
        [OrderAttribute(Order = 5)]
        CorporateDataFormDocument_Delete = 803,

        [Display(Name = "مشاهده")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "/Admin/Corporate/QuestionLevel")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "سطح سوالات")]
        [OrderAttribute(Order = 5)]
        CorporateQuestionLevel = 127,

        [Display(Name = "ویرایش و افزودن")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "سطح سوالات")]
        [OrderAttribute(Order = 5)]
        CorporateQuestionLevel_Save = 907,

        [Display(Name = "حذف")]
        [Category("BaseInfo")]
        [DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "سطح سوالات")]
        [OrderAttribute(Order = 5)]
        CorporateQuestionLevel_Delete = 807,

        //[Display(Name = "مشاهده درخواست ها")]
        //[Category("BaseInfo")]
        //[DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        //[DisplayFiledAttribute1(Name = "")]
        //[DisplayFiledAttribute2(Name = "fa fa-th-large")]
        //[DisplayFiledAttribute3(Name = "درخواست ها")]
        //[OrderAttribute(Order = 1)]
        //Corporate_Request = 905,

        //[Display(Name = "گزارش درخواست ها")]
        //[Category("BaseInfo")]
        //[DisplayFiledAttribute(Name = "حاکمیت شرکتی")]
        //[DisplayFiledAttribute1(Name = "")]
        //[DisplayFiledAttribute2(Name = "fa fa-th-large")]
        //[DisplayFiledAttribute3(Name = "گزارش ها")]
        //[OrderAttribute(Order = 1)]
        //Corporate_Report = 906,

        #endregion

        //[Display(Name = "مشاهده")]
        //[Category("IndexPanel")]
        //[DisplayFiledAttribute(Name = "داشبورد پنل")]
        //[DisplayFiledAttribute1(Name = "")]
        //[DisplayFiledAttribute2(Name = "fa fa-th-large")]
        //[DisplayFiledAttribute3(Name = "نمایش باکس")]
        //[OrderAttribute(Order = 5)]
        //DisplayBoxInAdminPanel = 733,
    }
}
