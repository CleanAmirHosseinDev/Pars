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

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [Display(Name = "مشاهده")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "/Admin/Users/Index")]
        [DisplayFiledAttribute2(Name = "fa fa-users")]
        [DisplayFiledAttribute3(Name = "کاربران سیستم")]
        [OrderAttribute(Order = 1)]
        Users = 102,

        [Display(Name = "ویرایش و افزودن")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-users")]
        [DisplayFiledAttribute3(Name = "کاربران سیستم")]
        [OrderAttribute(Order = 1)]
        Users_Save = 103,

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [Display(Name = "مشاهده و ویرایش و افزودن")]
        [Category("SystemStructure")]
        [DisplayFiledAttribute(Name = "ساختار سیستم")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-users")]
        [DisplayFiledAttribute3(Name = "تنظیم سطوح دسترسی")]
        [OrderAttribute(Order = 1)]
        SetAccessLevels = 104,

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        #endregion

        #region تعاریف پایه

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [Display(Name = "مشاهده")]
        [Category("BasicDefinitions")]
        [DisplayFiledAttribute(Name = "تعاریف پایه")]
        [DisplayFiledAttribute1(Name = "/area/state/index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تعریف استان")]
        [OrderAttribute(Order = 2)]
        State = 105,

        [Display(Name = "ویرایش و افزودن")]
        [Category("BasicDefinitions")]
        [DisplayFiledAttribute(Name = "تعاریف پایه")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تعریف استان")]
        [OrderAttribute(Order = 2)]
        State_Save = 106,

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [Display(Name = "مشاهده")]
        [Category("BasicDefinitions")]
        [DisplayFiledAttribute(Name = "تعاریف پایه")]
        [DisplayFiledAttribute1(Name = "/area/city/index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تعریف شهر")]
        [OrderAttribute(Order = 2)]
        City = 107,

        [Display(Name = "ویرایش و افزودن")]
        [Category("BasicDefinitions")]
        [DisplayFiledAttribute(Name = "تعاریف پایه")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تعریف شهر")]
        [OrderAttribute(Order = 2)]
        City_Save = 108,

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Display(Name = "مشاهده")]
        [Category("BasicDefinitions")]
        [DisplayFiledAttribute(Name = "تعاریف پایه")]
        [DisplayFiledAttribute1(Name = "/area/systemSeting/index")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تنظیمات سیستم")]
        [OrderAttribute(Order = 2)]
        SystemSeting = 109,

        [Display(Name = "ویرایش و افزودن")]
        [Category("BasicDefinitions")]
        [DisplayFiledAttribute(Name = "تعاریف پایه")]
        [DisplayFiledAttribute1(Name = "")]
        [DisplayFiledAttribute2(Name = "fa fa-th-large")]
        [DisplayFiledAttribute3(Name = "تنظیمات سیستم")]
        [OrderAttribute(Order = 2)]
        SystemSeting_Save = 110,

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        #endregion
    }
}
