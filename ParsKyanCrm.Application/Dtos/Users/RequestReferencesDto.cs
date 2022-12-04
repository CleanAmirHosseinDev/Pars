using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.Users
{
    public class RequestReferencesDto
    {
        public int ReferenceId { get; set; }
        public int? Requestid { get; set; }
        public int? DestLevelStepIndex { get; set; }        
        public DateTime? SendTime { get; set; }
        public string Comment { get; set; }
        public int? SendUser { get; set; }

        public string LevelStepAccessRole { get; set; }

        public RequestForRatingDto Request { get; set; }
        public UsersDto SendUserNavigation { get; set; }
    }
}
