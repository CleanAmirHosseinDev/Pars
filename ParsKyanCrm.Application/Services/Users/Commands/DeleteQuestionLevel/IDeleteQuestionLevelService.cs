using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteQuestionLevel
{
    public interface IDeleteQuestionLevelService
    {
        ResultDto Execute(int id);
    }
}
