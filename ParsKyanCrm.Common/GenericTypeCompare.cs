using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common
{
    public class GenericTypeCompare<T> : IEqualityComparer<T> where T : class
    {

        private string _fieldName;
        public GenericTypeCompare(string fieldName)
        {
            _fieldName = fieldName;
        }

        public bool Equals(T x, T y)
        {
            try
            {
                return x.GetType().GetProperty(_fieldName).GetValue(x).Equals(y.GetType().GetProperty(_fieldName).GetValue(y));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetHashCode(T obj)
        {
            try
            {
                return obj.GetType().GetProperty(_fieldName).GetValue(obj).GetHashCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
