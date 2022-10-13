using Microsoft.AspNetCore.Mvc.Rendering;
using ParsKyanCrm.Common.Attributes;
using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Infrastructure
{
    public static class EnumOperation<TEnum> where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        public static SelectList ToSelectList(TEnum? obj = null)
        {
            try
            {
                var q = new SelectList(Enum.GetValues(typeof(TEnum))
                    .OfType<Enum>()
                    .Select(x => new SelectListItem
                    {
                        Text = GetDisplayValue((TEnum)Enum.Parse(typeof(TEnum), (Convert.ToInt32(x)).ToString())),
                        Value = (Convert.ToInt32(x)).ToString(),
                        Selected = obj.HasValue ? Enum.GetName(typeof(TEnum), x) == obj.Value.ToString() : false
                    }), "Value", "Text");
                return q;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<NormalJsonClassDto> ToSelectListByGroup(TEnum? obj, List<string> SelectedList)
        {
            try
            {
                var q = ToSelectList(obj);

                List<NormalJsonClassDto> qRes = new List<NormalJsonClassDto>();
                if (q != null)
                {
                    foreach (var item in q)
                    {
                        qRes.Add(new NormalJsonClassDto()
                        {
                            Text = item.Text,
                            Value = item.Value,
                            Selected = SelectedList.Contains(item.Value),
                            Group = GetCategory((TEnum)Enum.Parse(typeof(TEnum), (Convert.ToInt32(item.Value)).ToString())),
                            LabelGroup = GetDisplayValue_CategoryName((TEnum)Enum.Parse(typeof(TEnum), (Convert.ToInt32(item.Value)).ToString())),
                            Link = GetDisplayValue_1((TEnum)Enum.Parse(typeof(TEnum), (Convert.ToInt32(item.Value)).ToString())),
                            Icon = GetDisplayValue_2((TEnum)Enum.Parse(typeof(TEnum), (Convert.ToInt32(item.Value)).ToString())),
                            Order = GetOrder((TEnum)Enum.Parse(typeof(TEnum), (Convert.ToInt32(item.Value)).ToString())),
                            Group_Item = GetDisplayValue_3((TEnum)Enum.Parse(typeof(TEnum), (Convert.ToInt32(item.Value)).ToString()))
                        });
                    }
                }

                return qRes.OrderBy(p => p.Order).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetDisplayValue(TEnum value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());

                if (fieldInfo == null) return string.Empty;

                var descriptionAttributes = fieldInfo.GetCustomAttributes(
                    typeof(DisplayAttribute), false) as DisplayAttribute[];

                if (descriptionAttributes == null) return string.Empty;
                return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetOrder(TEnum value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());

                if (fieldInfo == null) return string.Empty;

                var descriptionAttributes = fieldInfo.GetCustomAttributes(
                    typeof(OrderAttribute), false) as OrderAttribute[];

                if (descriptionAttributes == null) return string.Empty;
                return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Order.ToString() : value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetCategory(TEnum value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());

                if (fieldInfo == null) return string.Empty;

                var categoryAttributeAttributes = fieldInfo.GetCustomAttributes(
                    typeof(CategoryAttribute), false) as CategoryAttribute[];

                if (categoryAttributeAttributes == null) return string.Empty;
                return (categoryAttributeAttributes.Length > 0) ? categoryAttributeAttributes[0].Category : string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetDisplayValue_CategoryName(TEnum value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());

                if (fieldInfo == null) return string.Empty;

                var descriptionAttributes = fieldInfo.GetCustomAttributes(
                    typeof(DisplayFiledAttribute), false) as DisplayFiledAttribute[];

                if (descriptionAttributes == null) return string.Empty;
                return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetDisplayValue_1(TEnum value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());

                if (fieldInfo == null) return string.Empty;

                var descriptionAttributes = fieldInfo.GetCustomAttributes(
                    typeof(DisplayFiledAttribute1), false) as DisplayFiledAttribute1[];

                if (descriptionAttributes == null) return string.Empty;
                return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetDisplayValue_2(TEnum value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());

                if (fieldInfo == null) return string.Empty;

                var descriptionAttributes = fieldInfo.GetCustomAttributes(
                    typeof(DisplayFiledAttribute2), false) as DisplayFiledAttribute2[];

                if (descriptionAttributes == null) return string.Empty;
                return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetDisplayValue_3(TEnum value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());

                if (fieldInfo == null) return string.Empty;

                var descriptionAttributes = fieldInfo.GetCustomAttributes(
                    typeof(DisplayFiledAttribute3), false) as DisplayFiledAttribute3[];

                if (descriptionAttributes == null) return string.Empty;
                return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
