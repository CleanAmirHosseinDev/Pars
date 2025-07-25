﻿using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class RequestSystemSetingDto : PageingParamerDto
    {
        public int? SystemSetingId { get; set; }

        public int? ParentCode { get; set; }

        public string ParentCodeArr { get; set; }

    }

    public class SystemSetingDto : BaseEntityDto
    {

        public string ConfigKindOfRequestInitilizeOne { get; set; }

        public string NewLabel { get; set; }

        public int SystemSetingId { get; set; }
        public string Label { get; set; }
        public int? LabeCode { get; set; }
        public string Value { get; set; }        
        public int? BaseAmount { get; set; }
        public string TitleBaseAmount { get; set; }
        public double? FromAmount { get; set; }
        public double? ToAmount { get; set; }
        public int? ChangeBy { get; set; }
        public DateTime? ChangeDate { get; set; }
        public int? ParentCode { get; set; }
        public string ParenLabel { get; set; }
    }
}
