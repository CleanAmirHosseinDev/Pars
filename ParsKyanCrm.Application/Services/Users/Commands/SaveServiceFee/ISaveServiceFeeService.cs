using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveServiceFee
{
    public interface ISaveServiceFeeService
    {
        Task<ResultDto<ServiceFeeDto>> Execute(ServiceFeeDto request);
    }
}
