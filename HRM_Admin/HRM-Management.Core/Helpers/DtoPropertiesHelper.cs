using HRM_Management.Core.Helpers.Enums;
using System.Text;

namespace HRM_Management.Core.Helpers
{
    public static class DtoPropertiesHelper
    {
        public static (string FName, string LName, string MName) SplitFullName(this string fullName, NameFormat nameFormat)
        {
            var parts = fullName.Split(' ');
            return nameFormat switch
            {
                NameFormat.English => (parts[0], parts[2], parts[1]),
                NameFormat.Ukrainian => (parts[1], parts[0], parts[2])// en: F M L  uk: L F M
            };
        }

        public static string UniteFullName(string FName, string LName, string MName, NameFormat nameFormat)
        {
            return nameFormat switch
            {
                NameFormat.English => $"{FName} {LName} {MName}",
                NameFormat.Ukrainian => $"{LName} {FName} {MName}"// en: F M L  uk: L F M
            };
        }

        public static string ToShortData(string splitter, params string[] strings)
        {
            var builder = new StringBuilder();
            foreach (var item in strings)
            {
                builder.Append(splitter);
                builder.Append(item);
            }
            return builder.ToString();
        }

        public static void HandleLeaderIds<TDestination>(TDestination dest)
            where TDestination : class
        {
            if (dest == null) return;

            foreach (var propName in Constants.LEADERS_IDS_COLUMN_NAMES)
            {
                var property = typeof(TDestination).GetProperty(propName);
                if (property != null && property.PropertyType == typeof(int?))
                {
                    var value = (int?)property.GetValue(dest);
                    if (value == 0)
                    {
                        property.SetValue(dest, null);
                    }
                }
            }
        }
              
        public static string UniteFirstAndLastName(string FName, string LName)
        {
            return $"{FName} {LName}";
        }

        public static string WriteNamesInAList(params string[] names)
        {
            if (names.Length == 1)
                return names[0];

            var sb = new StringBuilder(names[0]);
            for (var i = 1; i < names.Length; i++)
            {
                if (i == names.Length - 1)
                {
                    sb.Append($" and {names[i]}");
                    continue;
                }
                sb.Append($", {names[i]}");
            }
            return sb.ToString();
        }
        
        public static string GetFirstTechStackWord(string techStack)
        {
            if (string.IsNullOrEmpty(techStack))
                return string.Empty;

            var parts = techStack.Split(',');
            return parts.Length > 0 ? parts[0] : string.Empty;
        }
    }
}
