using ParsKyanCrm.Common.Dto;
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
        public ResultDto Execute(RequestSaveAssessmentDto request)
        {
            try
            {

                request.RequestID = request.RequestID.Decrypt_Advanced_For_Number();

                Ado_NetOperation.SqlUpdate("", new Dictionary<string, object>()
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
