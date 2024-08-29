using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveCustomerRequestInformation
{
    public interface ISaveCustomerRequestInformationService
    {
        Task<ResultDto<CustomerRequestInformationsDto>> Execute(CustomerRequestInformationsDto request);
    }
}
