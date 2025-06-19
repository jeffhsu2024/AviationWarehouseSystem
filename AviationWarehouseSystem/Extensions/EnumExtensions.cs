using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AviationWarehouseSystem.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 獲取 Enum 的 Display 名稱
        /// </summary>
        /// <param name="enumValue">Enum 值</param>
        /// <returns>Display 名稱，如果沒有則返回 Enum 名稱</returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Name ?? enumValue.ToString();
        }

        /// <summary>
        /// 獲取 Enum 的 Display 描述
        /// </summary>
        /// <param name="enumValue">Enum 值</param>
        /// <returns>Display 描述，如果沒有則返回 Enum 名稱</returns>
        public static string GetDisplayDescription(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Description ?? enumValue.ToString();
        }
    }
}
