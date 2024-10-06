using ParsKyanCrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class RequestQuestionLevelDto : PageingParamerDto
    {
        public int? QuestionLevelId { get; set; }
    }

    public class QuestionLevelDto
    {
        public int QuestionLevelId { get; set; }
        public string LevelTitle { get; set; }
        public string LevelDescription { get; set; }
        public byte? IsActive { get; set; }
    }
}
