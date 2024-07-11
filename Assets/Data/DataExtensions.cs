using UnityEngine;

namespace Data
{
    public static class DataExtensions
    {
        public static Vector3 AddX(this Vector3 vector, float x)
        {
            vector.x += x;
            return vector;
        }
        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y += y;
            return vector;
        }

        public static string ToJson(this object obj) => 
            JsonUtility.ToJson(obj);
        public static T ToDeserialized<T>(this string json) => 
            JsonUtility.FromJson<T>(json);

        public static T GetEnumType<T>(this System.Random random)
        {
            var type = typeof(T);
            var values = type.GetEnumValues();
            var index = random.Next(values.Length);
            return (T)values.GetValue(index);
        }
    }
}
