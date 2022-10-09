using ParsKyanCrm.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Queries.GetAccessLevels
{
    public interface IGetAccessLevelsService
    {
        Task<List<NormalJsonClassDto>> Execute(int id);
    }
}
