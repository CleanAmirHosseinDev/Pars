using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteDataFormAnswerTables
{

    public class DeleteRequestForRatingService : IDeleteRequestForRatingService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;        
        private readonly IWebHostEnvironment _env;

        public DeleteRequestForRatingService(IDataBaseContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;            
            _env = env;
        }

        public ResultDto Execute(int id)
        {

            try
            {

                Ado_NetOperation.SqlDelete(typeof(Domain.Entities.RequestReferences).Name, string.Format("Requestid" + " = '{0}'", id));
                Ado_NetOperation.SqlDelete(typeof(Domain.Entities.RequestForRating).Name, string.Format("Requestid" + " = '{0}'", id));


                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "درخواست با موفقیت حذف شد"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
