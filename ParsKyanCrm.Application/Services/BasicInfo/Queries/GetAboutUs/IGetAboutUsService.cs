using ParsKyanCrm.Application.Dtos.BasicInfo;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.BasicInfo.Queries.GetAboutUs
{
    public interface IGetAboutUsService
    {
        Task<AboutUsDto> Execute();
    }
}
