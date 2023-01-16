using ParsKyanCrm.Application.Dtos.Users;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetServiceFeeAndCustomerByRequest
{
    public class ResultGetServiceFeeAndCustomerByRequestDto
    {
        public CustomersDto Customers { get; set; }

        public ServiceFeeDto ServiceFee { get; set; }

    }
}
