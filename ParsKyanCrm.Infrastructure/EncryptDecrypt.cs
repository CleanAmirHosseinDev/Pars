using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Infrastructure
{
    public static class EncryptDecrypt
    {
        public static string Encrypt(this string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str)) return null;

                byte[] encData_byte = new byte[str.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(str);
                return Convert.ToBase64String(encData_byte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Decrypt(this string str)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(str);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                return new string(decoded_char);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string Encrypt_Advanced_For_Number(this string str)
        {
            try
            {
                int r = int.Parse(str);
                string s = "elfbdkaf-hkse-dbbf-sdcz-csacfuaccvbbelfbdkaf-hkse-dbbf-sdcz-csacfuaccvbbelfbdkaf-hkse-dbbf-sdcz-csacfuaccvbbelfbdkaf-hkse-dbbf-sdcz-csacfuaccvbbelfbdkaf-hkse-dbbf-sdcz-csacfuaccvbb" + (r + int.Parse(((6963 * 5) * (10 / 2) + 2568 - 10).ToString())).ToString() + "elfbdkaf-hkse-dbbf-sdcz-csacfuaccvbbelfbdkaf-hkse-dbbf-sdcz-csacfuaccvbbelfbdkaf-hkse-dbbf-sdcz-csacfuaccvbbelfbdkaf-hkse-dbbf-sdcz-csacfuaccvbbelfbdkaf-hkse-dbbf-sdcz-csacfuaccvbb";
                return s.Encrypt();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string Decrypt_Advanced_For_Number(this string str)
        {
            try
            {
                string strResFinal = str.Decrypt().Replace("elfbdkaf-hkse-dbbf-sdcz-csacfuaccvbbelfbdkaf-hkse-dbbf-sdcz-csacfuaccvbbelfbdkaf-hkse-dbbf-sdcz-csacfuaccvbbelfbdkaf-hkse-dbbf-sdcz-csacfuaccvbbelfbdkaf-hkse-dbbf-sdcz-csacfuaccvbb", "");
                int r = int.Parse(strResFinal);
                int res = r - int.Parse(((6963 * 5) * (10 / 2) + 2568 - 10).ToString());
                return res.ToString();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
