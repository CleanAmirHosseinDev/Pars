using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using ParsKyanCrm.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveCustomerRequestInformation
{

    public class SaveCustomerRequestInformationService : ISaveCustomerRequestInformationService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SaveCustomerRequestInformationService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        private bool Check_Remote(CustomerRequestInformationsDto request)
        {
            try
            {
                string strCondition = string.Empty;

                if (request.RequestId != 0)
                {
                    strCondition = " " + nameof(request.RequestId) + " = " + request.RequestId;
                }

                if (!string.IsNullOrEmpty(strCondition))
                {
                    var q = Ado_NetOperation.GetAll_Table(nameof(CustomerRequestInformation), "*", strCondition);
                    return q != null && q.Rows.Count > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResultDto<CustomerRequestInformationsDto>> Execute(CustomerRequestInformationsDto request)
        {
            #region Upload Image Check
            string fileNameOldPic_LastInsuranceList = string.Empty, path_LastInsuranceList = string.Empty;
            string fileNameOldPic_LastAuditingTaxList = string.Empty, path_LastAuditingTaxList = string.Empty;

            var cus = await _context.CustomerRequestInformation.FindAsync(request.RequestId);
            request.LastInsuranceList = cus != null && !string.IsNullOrEmpty(cus.LastInsuranceList) ? cus.LastInsuranceList : string.Empty;
            request.LastAuditingTaxList = cus != null && !string.IsNullOrEmpty(cus.LastAuditingTaxList) ? cus.LastAuditingTaxList : string.Empty;
            #endregion

            #region Upload Image
            if (request.Result_Final_LastInsuranceList != null)
            {
                fileNameOldPic_LastInsuranceList = request.LastInsuranceList;
                request.LastInsuranceList = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LastInsuranceList.FileName);
                path_LastInsuranceList = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.LastInsuranceList;
                await ServiceFileUploader.SaveFile(request.Result_Final_LastInsuranceList, path_LastInsuranceList, "لیست آخرین بیمه");
            }
            if (request.Result_Final_LastAuditingTaxList != null)
            {
                fileNameOldPic_LastAuditingTaxList = request.LastAuditingTaxList;
                request.LastAuditingTaxList = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(request.Result_Final_LastAuditingTaxList.FileName);
                path_LastAuditingTaxList = _env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + request.LastAuditingTaxList;
                await ServiceFileUploader.SaveFile(request.Result_Final_LastAuditingTaxList, path_LastAuditingTaxList, "لیست آخرین تغییرات صورت های حسابرسی یا اضهار نامه مالیاتی");
            }
            #endregion

            EntityEntry<CustomerRequestInformation> q_Entity;

            if (Check_Remote(request) == false)
            {
                request.IsActive = 15;
                q_Entity = _context.CustomerRequestInformation.Add(_mapper.Map<CustomerRequestInformation>(request));
                await _context.SaveChangesAsync();
                request = _mapper.Map<CustomerRequestInformationsDto>(q_Entity.Entity);
            }
            else
            {
                Ado_NetOperation.SqlUpdate(typeof(CustomerRequestInformation).Name, new Dictionary<string, object>()
                {
                    {
                        nameof(request.LastInsuranceList),request.LastInsuranceList
                    },
                    {
                        nameof(request.CountOfPersonel),request.CountOfPersonel
                    },
                    {
                        nameof(request.AmountOfLastSale),request.AmountOfLastSale
                    },
                    {
                        "LastAuditingTaxList",request.LastAuditingTaxList
                    },
                }, string.Format(nameof(q_Entity.Entity.RequestId) + $" = {request.RequestId}"));
            }
            #region Upload Image

            if (request.Result_Final_LastInsuranceList != null)
                FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_LastInsuranceList);

            if (request.Result_Final_LastAuditingTaxList != null)
                FileOperation.DeleteFile(_env.ContentRootPath + VaribleForName.CustomersFolderWithwwwroot + fileNameOldPic_LastAuditingTaxList);

            #endregion

            return new ResultDto<CustomerRequestInformationsDto>()
            {
                IsSuccess = true,
                Data = request,
                Message = "مشتری موردنظر با موفقیت ثبت شد"
            };
        }
    }
}
