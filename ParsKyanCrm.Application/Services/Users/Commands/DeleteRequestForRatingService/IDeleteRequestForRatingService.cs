using ParsKyanCrm.Common.Dto;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteDataFormAnswerTables
{
    public interface IDeleteRequestForRatingService
    {
        ResultDto Execute(int id);
    }
}
