using FluentValidation.Results;
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

        public static string GetErrorsF(this List<ValidationFailure> modelState)
        {
            try
            {
                if (modelState != null && modelState.Count() > 0) return string.Join("\n", modelState.FirstOrDefault().ErrorMessage);

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
