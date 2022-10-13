using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common
{
    public static class FileOperation
    {
        public static bool ExistsFile(string fullName)
        {
            try
            {
                return File.Exists(fullName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteFile(string fullName)
        {
            try
            {

                if (!string.IsNullOrEmpty(fullName))
                {
                    if (ExistsFile(fullName)) File.Delete(fullName);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
