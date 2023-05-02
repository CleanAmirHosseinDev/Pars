using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Domain.Entities;
using System.Collections.Generic;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetServiceFeeAndCustomerByRequest
{
    public class ResultGetServiceFeeAndCustomerByRequestDto
    {
        public CustomersDto Customers { get; set; }

        public ServiceFeeDto ServiceFee { get; set; }

        public ContractDto Contract { get; set; }
        public List<ContractPages> ContractPage { get; set; }


    }
}
