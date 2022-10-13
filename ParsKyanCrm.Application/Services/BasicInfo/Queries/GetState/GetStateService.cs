using AutoMapper;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetState
{

    public class GetStateService : IGetStateService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public GetStateService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StateDto> Execute(int? id = null)
        {
            try
            {

                StateDto res = new StateDto();

                if (id != null && id != 0)
                {
                    var q_Find = await _context.States.FindAsync(id);
                    res = _mapper.Map<StateDto>(q_Find);
                }

                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
