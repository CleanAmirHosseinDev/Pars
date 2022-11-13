using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common.Enums
{
    public enum RequestForRatingStatus : int
    {


        [Display(Name = "در دست بررسی")]
        UnderInvestigation = 0,

        [Display(Name = "اجازه بارگزاری قرارداد و مشاهده قرارداد")]
        PermissionUploadContractViewContract = 1,

        [Display(Name = "اجازه بارگزاری مدارک تکمیلی")]
        PermissionUploadAdditionalDocuments = 2
    }
}
