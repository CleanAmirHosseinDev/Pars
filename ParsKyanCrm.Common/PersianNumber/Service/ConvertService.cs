using ParsKyanCrm.Common.PersianNumber.Extension;
using ParsKyanCrm.Common.PersianNumber.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParsKyanCrm.Common.PersianNumber.Service
{
    class ConvertService
{
    public static string EnglishToPersian(string input)
    {
        try
        {
            return input.ConvertEnglishNumberToPersianNumber();
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
            return input.ConvertPersianNumberToEnglishNumber();
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
            return System.Convert.ToInt32(input.ConvertPersianNumberToEnglishNumber());
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
            return System.Convert.ToInt64(input.ConvertPersianNumberToEnglishNumber());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static float PersianToEnglishFloat(string input)
    {
        try
        {
            return System.Convert.ToSingle(input.ConvertPersianNumberToEnglishNumber());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static double PersianToEnglishDouble(string input)
    {
        try
        {
            return System.Convert.ToDouble(input.ConvertPersianNumberToEnglishNumber());
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
            return System.Convert.ToDecimal(input.ConvertPersianNumberToEnglishNumber());
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
            return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(input.ConvertPersianNumberToEnglishNumber()));
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
            return Encoding.UTF8.GetString(System.Convert.FromBase64String(base64)).ConvertEnglishNumberToPersianNumber();
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
            return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
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
            return Encoding.UTF8.GetString(System.Convert.FromBase64String(base64)).ConvertPersianNumberToEnglishNumber();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static string NumberToText(string input)
    {
        try
        {
            return PersianText.PersianNumberToString(input);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
}
