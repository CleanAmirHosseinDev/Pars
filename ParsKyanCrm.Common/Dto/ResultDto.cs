using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common.Dto
{
    public class ResultDto
    {
        // اگر فرم ایدی داشت آن را بر می گرداند
        public int ResultId { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }

    public class ResultDto<T>
    {
        // اگر فرم ایدی داشت آن را بر می گرداند
        public int DataId { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public long Rows { get; set; }

        public int StatusCode { get; set; }

    }
}
