using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveBasicInformationCustomers
{
    public interface ISaveBasicInformationCustomersService
    {
        ResultDto Execute(CustomersDto request);
    }
}
