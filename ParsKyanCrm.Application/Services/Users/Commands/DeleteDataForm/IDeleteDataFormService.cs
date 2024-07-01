using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.DeleteDataForm
{
    public interface IDeleteDataFormService
    {
        ResultDto Execute(int id);
    }
}
