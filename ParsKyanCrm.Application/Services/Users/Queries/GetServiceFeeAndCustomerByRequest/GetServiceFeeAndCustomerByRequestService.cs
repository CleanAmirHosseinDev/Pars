using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetServiceFeeAndCustomerByRequest
{

    public class GetServiceFeeAndCustomerByRequestService : IGetServiceFeeAndCustomerByRequestService
    {

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        public GetServiceFeeAndCustomerByRequestService(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
        }

        public async Task<ResultGetServiceFeeAndCustomerByRequestDto> Execute(int ri)
        {

            try
            {
                var qRequest = await _context.RequestForRating.FindAsync(ri);

                var qCustomer = await _context.Customers.FindAsync(qRequest.CustomerId);

                var qContract = await _context.Contract.FirstOrDefaultAsync(m => m.KinfOfRequest == qRequest.KindOfRequest && m.IsActive == 15);

                List<ContractPages> contractPage = await _context.ContractPages.Where(m=>m.ContractId==qContract.ContractId).ToListAsync();

                int cOP = qCustomer.CountOfPersonal.HasValue ? qCustomer.CountOfPersonal.Value : 0;

                var qServiceFee = await _context.ServiceFee.FirstOrDefaultAsync(p => p.IsActive == (byte)Common.Enums.TablesGeneralIsActive.Active && p.KindOfService == qRequest.KindOfRequest && (cOP >= p.FromCompanyRange && cOP <= p.ToCompanyRange));
 

                if ( qContract==null)
                {
                    return new ResultGetServiceFeeAndCustomerByRequestDto()
                    {
                        Contract = null,
                        Customers = null,
                        ServiceFee = null
                    };
                }

                // جا گذاری مقادیر در متن قرارداد
               
                var qKindOfCompany = await _context.SystemSeting.FindAsync(qCustomer.KindOfCompanyId);

                string strNamesAuthorizedSignatoriesValue = "";

                if (!string.IsNullOrEmpty(qCustomer.NamesAuthorizedSignatories))
                {
                    string[] NA = qCustomer.NamesAuthorizedSignatories.Split(",");
                    foreach (string N in NA)
                        strNamesAuthorizedSignatoriesValue += N + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"; 
                }

                string strContract = "<div class='row text-right'>";
                if (qRequest.KindOfRequest==66)
                {
                    int i = 0;
                    foreach (var item in contractPage)
                    {
                        strContract +=(i==0? "<div Class='PageContractFirst'>" : "<div Class='PageContract'>");
                        i++;
                        #region contractPageText
                         strContract += item.ContractText;

                        strContract = strContract.Replace("CompanyNameValue", (qCustomer.CompanyName == null ? "" : qCustomer.CompanyName));
                        if (qKindOfCompany != null)
                        {
                            strContract = strContract.Replace("KindOfCompanyValue", (qKindOfCompany.Label == null ? "" : qKindOfCompany.Label));
                            strContract = strContract.Replace("KindOfCompany2Value", (qKindOfCompany.Label == null ? "" : qKindOfCompany.Label));

                        }
                        else
                        {
                            strContract = strContract.Replace("KindOfCompanyValue", "");
                            strContract = strContract.Replace("KindOfCompany2Value", "");

                        }
                        strContract = strContract.Replace("EconomicCodeValue", (qCustomer.EconomicCode == null ? "" : qCustomer.EconomicCode));
                        strContract = strContract.Replace("NationalCodeValue", (qCustomer.NationalCode == null ? "" : qCustomer.NationalCode));
                        strContract = strContract.Replace("AddressCompanyValue", (qCustomer.AddressCompany == null ? "" : qCustomer.AddressCompany));
                        strContract = strContract.Replace("PostalCodeValue", (qCustomer.PostalCode == null ? "" : qCustomer.PostalCode));
                        strContract = strContract.Replace("AgentNameValue", (qCustomer.CeoName == null ? qCustomer.CompanyName : qCustomer.CeoName));
                        strContract = strContract.Replace("EmailValue", (qCustomer.Email == null ? "" : qCustomer.Email));
                        strContract = strContract.Replace("CompanyName2Value", (qCustomer.CompanyName == null ? "" : qCustomer.CompanyName));

                        strContract = strContract.Replace("AgentName2Value", (qCustomer.AgentName == null ? "" : qCustomer.AgentName));
                        strContract = strContract.Replace("AgentMobileValue", (qCustomer.AgentMobile == null ? "" : qCustomer.AgentMobile));
                        strContract = strContract.Replace("EmailRepresentativeValue", (qCustomer.EmailRepresentative == null ? qCustomer.Email : qCustomer.EmailRepresentative));

                        strContract = strContract.Replace("NamesAuthorizedSignatoriesValue", (strNamesAuthorizedSignatoriesValue));
                        strContract = strContract.Replace("NamesAuthorizedSignatories2Value", (strNamesAuthorizedSignatoriesValue));
                        strContract = strContract.Replace("CompanyName3Value", (qCustomer.CompanyName == null ? "" : qCustomer.CompanyName));
                        strContract = strContract.Replace("NationalCode2Value", (qCustomer.NationalCode == null ? "" : qCustomer.NationalCode));
                        strContract = strContract.Replace("CompanyName4Value", (qCustomer.CompanyName == null ? "" : qCustomer.CompanyName));

                        strContract = strContract.Replace("NamesAuthorizedSignatories2Value", (qCustomer.NamesAuthorizedSignatories == null ? "" : qCustomer.NamesAuthorizedSignatories));
                        strContract = strContract.Replace("CountOfPersonalValue", qCustomer.CountOfPersonal.Value.ToString());
                        strContract = strContract.Replace("TelValue", qCustomer.Tel);

                        decimal tax = 0;
                        decimal Price = (qServiceFee != null ? Convert.ToDecimal(CalcContractPrice(qCustomer, qServiceFee)) : 0);
                        decimal FinalPrice=(Price-Convert.ToDecimal(Price.ToString().Substring(Price.ToString().Length - 5)));
                        tax = Math.Round((Price!=0 ? FinalPrice * 9 : 0) / 100, 0);

                        // مبلع فرمول
                        strContract = strContract.Replace("ServiceFeePriceValue", qServiceFee != null ? CalcContractPrice(qCustomer, qServiceFee) : "0");
                        qContract.ContractText = strContract;

                        strContract = strContract.Replace("TaxValue", double.Parse((tax.ToString()).ToString()).ToString("N0"));
                        strContract = strContract.Replace("TotalPriceValue", double.Parse(((FinalPrice + tax).ToString()).ToString()).ToString("N0"));

                        item.ContractText = strContract;
                        #endregion
                        strContract += "</div>";
                    }

                }
                strContract += "</div>";
                qContract.ContractText = strContract;
                    if (qServiceFee == null)
                    {
                    return new ResultGetServiceFeeAndCustomerByRequestDto()
                    {
                          
                            Contract = _mapper.Map<ContractDto>(qContract != null ? qContract : new Contract()),
                            Customers = _mapper.Map<CustomersDto>(qCustomer),
                            ServiceFee = null,
                    };
                   }
                return new ResultGetServiceFeeAndCustomerByRequestDto()
                {
                    
                    Customers = _mapper.Map<CustomersDto>(qCustomer),
                    ServiceFee = _mapper.Map<ServiceFeeDto>(qServiceFee != null ? qServiceFee : new ServiceFee()),
                    Contract = _mapper.Map<ContractDto>(qContract != null ? qContract : new Contract())
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string CalcContractPrice(Customers customers, ServiceFee serviceFee)
        {
            try
            {
                decimal Yek10000 = (customers.AmountOsLastSales.HasValue ? customers.AmountOsLastSales.Value : 0) / 10000;
                int countOfPer = customers.CountOfPersonal.HasValue ? customers.CountOfPersonal.Value : 0;
                if (serviceFee.Fee2 == null || serviceFee.Fee2 == 0)
                {

                    decimal N1 = countOfPer > 0 ? (serviceFee.FixedCost.HasValue ? serviceFee.FixedCost.Value : 0) : 0;
                    decimal N2 = Yek10000 < (serviceFee.Fee1.HasValue ? serviceFee.Fee1.Value : 0) ? Yek10000 : 0;
                    decimal N3 = (countOfPer * (serviceFee.VariableCost.HasValue ? serviceFee.VariableCost.Value : 0));



                    decimal Res = (Math.Round(N1 + N2 + N3, 0) / 1000000) * 1000000;

                    return double.Parse((Res).ToString()).ToString("N0"); 
                }
                else
                {
                    decimal N1 = (countOfPer * (serviceFee.VariableCost.HasValue ? serviceFee.VariableCost.Value : 0)) > (serviceFee.Fee1.HasValue ? serviceFee.Fee1.Value : 0) ? (serviceFee.Fee1.HasValue ? serviceFee.Fee1.Value : 0) : (countOfPer * (serviceFee.VariableCost.HasValue ? serviceFee.VariableCost.Value : 0));
                    decimal N2 = countOfPer > 0 ? (serviceFee.FixedCost.HasValue ? serviceFee.FixedCost.Value : 0) : 0;
                    decimal N3 = Yek10000 < (serviceFee.Fee2.HasValue ? serviceFee.Fee2.Value : 0) ? Yek10000 : (serviceFee.Fee2.HasValue ? serviceFee.Fee2.Value : 0);

                    decimal res = (Math.Round(N1 + N2 + N3, 0) / 1000000) * 1000000;
                    return double.Parse((res).ToString()).ToString("N0");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
