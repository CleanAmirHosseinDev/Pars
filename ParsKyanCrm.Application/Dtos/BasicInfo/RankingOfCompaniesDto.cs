using Microsoft.AspNetCore.Http;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Dtos.BasicInfo
{

    public class RequestRankingOfCompaniesDto : PageingParamerDto
    {

        public int? RankingId { get; set; }

    }

    public class RankingOfCompaniesDto : BaseEntityDto
    {

        public int RankingId { get; set; }
        public int? ComanyId { get; set; }
        public string PublishDate { get; set; }
        public string LongTermRating { get; set; }
        public string ShortTermRating { get; set; }
        public string Vision { get; set; }
        public string StatusText { get; set; }
        public string RankingTypeText { get; set; }
        public string PressRelease { get; set; }
        public string PressReleaseFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(PressRelease, VaribleForName.RankingOfCompaniesFolder, false);
            }
        }
        public IFormFile Result_Final_PressRelease { get; set; }

        public string SummaryRanking { get; set; }
        public string SummaryRankingFull
        {
            get
            {
                return ServiceFileUploader.GetFullPath(SummaryRanking, VaribleForName.RankingOfCompaniesFolder, false);
            }
        }
        public IFormFile Result_Final_SummaryRanking { get; set; }

        public int? UserId { get; set; }        

        public CompaniesDto Comany { get; set; }
        public UsersDto User { get; set; }

    }
}
