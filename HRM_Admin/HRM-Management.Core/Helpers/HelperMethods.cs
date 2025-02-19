using Quartz;
using System.Text;
using System.Text.Json;

namespace HRM_Management.Core.Helpers
{
    public static class HelperMethods
    {
        public static StringContent CreateJsonContent(object obj, Encoding encoding)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Object cannot be null.");

            var json = JsonSerializer.Serialize(obj);
            return new StringContent(json, encoding, "application/json");
        }

        public static TriggerKey ToTriggerKey(this string triggerKey)
        {
            var splitted = triggerKey.Split(".");
            if (splitted.Length != Constants.JOBKEY_PARTS_COUNT) throw new ArgumentException("Incorrect key format!");
            return new TriggerKey(splitted[1], splitted[0]);
        }
    }
}