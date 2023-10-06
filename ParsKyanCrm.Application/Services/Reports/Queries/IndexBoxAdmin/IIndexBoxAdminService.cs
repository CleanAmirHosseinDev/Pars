using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.IndexBoxAdmin
{
    public interface IIndexBoxAdminService
    {
        Task<ResultIndexBoxAdminDto> Execute();
    }
}
