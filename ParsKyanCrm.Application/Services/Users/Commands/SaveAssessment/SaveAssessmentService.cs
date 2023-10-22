using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveAssessment
{

    public class SaveAssessmentService : ISaveAssessmentService
    {

        private readonly IDataBaseContext _context;

        public SaveAssessmentService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<ResultDto> Execute(RequestSaveAssessmentDto request)
        {
            try
            {

                int requestID = int.Parse(request.RequestID);
                var q = await _context.RequestForRating.FindAsync(requestID);

                if (!string.IsNullOrEmpty(q.Assessment))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "شما قبلا در نظرسنجی شرکت کرده اید",
                    };
                }


                Ado_NetOperation.SqlUpdate("RequestForRating", new Dictionary<string, object>()
                    {
                        {
                            "Assessment",request.Assessment
                        },
                        {
                            "ReasonAssessment1",request.ReasonAssessment1
                        }
                    }, string.Format(" RequestID " + " = {0} ", request.RequestID));

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "با موفقیت ثبت شد",
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
