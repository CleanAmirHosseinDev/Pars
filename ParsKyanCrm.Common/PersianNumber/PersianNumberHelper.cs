using ParsKyanCrm.Common.PersianNumber.Exceptions;
using ParsKyanCrm.Common.PersianNumber.Extension;
using ParsKyanCrm.Common.PersianNumber.Service;
using System;
using System.Collections.Generic;
using System.Text;
namespace ParsKyanCrm.Common.PersianNumber
{
    public class PersianNumberHelper
    {
        public static string EnglishToPersian(string input)
        {
            try
            {
                return ConvertService.EnglishToPersian(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EnglishToPersian(int input)
        {
            try
            {
                return EnglishToPersian(input.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EnglishToPersian(long input)
        {
            try
            {
                return EnglishToPersian(input.ToString());
            }
            catch (Exception ex) { throw ex; }
        }

        public static string EnglishToPersian(float input)
        {
            try
            {
                return EnglishToPersian(input.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EnglishToPersian(double input)
        {
            try
            {
                return EnglishToPersian(input.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EnglishToPersian(decimal input)
        {
            try
            {
                return EnglishToPersian(input.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string PersianToEnglish(string input)
        {
            try
            {
                return ConvertService.PersianToEnglish(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int PersianToEnglishInt32(string input)
        {
            try
            {
                if (!input.IsNumber()) { throw new FormatException(Messages.InvalidNumberFormatException); }
                input = input.NormalizeNumber();
                if (!input.IsInteger()) { throw new FormatException(Messages.InvalidNumberFormatException); }
                return ConvertService.PersianToEnglishInt32(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static long PersianToEnglishInt64(string input)
        {
            try
            {
                if (!input.IsNumber()) { throw new FormatException(Messages.InvalidNumberFormatException); }
                input = input.NormalizeNumber();
                if (!input.IsInteger()) { throw new FormatException(Messages.InvalidNumberFormatException); }
                return ConvertService.PersianToEnglishInt64(input);
            }
            catch (Exception ex) { throw ex; }
        }

        public static float PersianToEnglishFloat(string input)
        {
            try
            {
                if (!input.IsNumber()) { throw new FormatException(Messages.InvalidNumberFormatException); }
                input = input.NormalizeNumber();
                return ConvertService.PersianToEnglishFloat(input);
            }
            catch (Exception ex) { throw ex; }
        }

        public static double PersianToEnglishDouble(string input)
        {
            try
            {
                if (!input.IsNumber()) { throw new FormatException(Messages.InvalidNumberFormatException); }
                input = input.NormalizeNumber();
                return ConvertService.PersianToEnglishDouble(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Decimal PersianToEnglishDecimal(string input)
        {
            try
            {
                if (!input.IsNumber()) { throw new FormatException(Messages.InvalidNumberFormatException); }
                input = input.NormalizeNumber();
                return ConvertService.PersianToEnglishDecimal(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string PersianToBase64(string input)
        {
            try
            {
                if (!input.IsNumber()) { throw new FormatException(Messages.InvalidNumberFormatException); }
                return ConvertService.PersianToBase64(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Base64ToPersian(string base64)
        {
            try
            {
                return ConvertService.Base64ToPersian(base64);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EnglishToBase64(string input)
        {
            try
            {
                if (!input.IsNumber()) { throw new FormatException(Messages.InvalidNumberFormatException); }
                return ConvertService.EnglishToBase64(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Base64ToEnglish(string base64)
        {
            try
            {
                return ConvertService.Base64ToEnglish(base64);
            }
            catch (Exception ex) { throw ex; }
        }
        public static string NumberToText(string input)
        {
            try
            {
                input = PersianToEnglish(input);
                if (!input.IsNumber()) { throw new FormatException(Messages.InvalidNumberFormatException); }
                input = input.NormalizeNumber();
                if (!input.IsInteger()) { throw new FormatException(Messages.InvalidNumberFormatException); }
                return ConvertService.NumberToText(input);
            }
            catch (Exception ex) { throw ex; }
        }
    }

}