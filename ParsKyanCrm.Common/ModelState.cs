using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common
{
    public static class ModelState
    {
        public static string GetErrors(this ModelStateDictionary modelState)
        {
            try
            {
                return string.Join("<br />", (from item in modelState
                                              where item.Value.Errors.Any()
                                              select item.Value.Errors[0].ErrorMessage).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
