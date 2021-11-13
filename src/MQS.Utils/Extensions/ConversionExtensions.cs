using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQS.Utils.Extensions
{
	public static class ConversionExtensions
	{
        public static T? ToEnum<T>(this object obj, bool ignoreCase = true) where T : struct
        {
            if (obj != null && Enum.TryParse(obj.ToString(), ignoreCase, out T result))
            {
                return result;
            }
            return null;
        }

        public static T ToEnum<T>(this object obj, T defaultValue) where T : struct
        {
            var result = obj.ToEnum<T>();
            return result ?? defaultValue;
        }

        public static bool IsEnumType<T>(this object obj) where T : struct
        {
            return ToEnum<T>(obj) != null;
        }

        public static T To<T>(this object value)
        {
            var conversionType = typeof(T);
            return (T)To(value, conversionType);
        }

        public static T To<T>(this object value, object defaultValue)
        {
            try
            {
                var conversionType = typeof(T);
                return (T)To(value, conversionType);
            }
            catch (Exception)
            {
                return (T)defaultValue;
            }
        }

        public static object To(this object value, Type conversionType)
        {
            // Note: This if block was taken from Convert.ChangeType as is, and is needed here since we're
            // checking properties on conversionType below.
            if (conversionType == null)
                throw new ArgumentNullException(nameof(conversionType));

            // If it's not a nullable type, just pass through the parameters to Convert.ChangeType

            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // It's a nullable type, so instead of calling Convert.ChangeType directly which would throw a
                // InvalidCastException (per http://weblogs.asp.net/pjohnson/archive/2006/02/07/437631.aspx),
                // determine what the underlying type is
                // If it's null, it won't convert to the underlying type, but that's fine since nulls don't really
                // have a type--so just return null
                // Note: We only do this check if we're converting to a nullable type, since doing it outside
                // would diverge from Convert.ChangeType's behavior, which throws an InvalidCastException if
                // value is null and conversionType is a value type.
                if (value == null)
                    return null;

                // It's a nullable type, and not null, so that means it can be converted to its underlying type,
                // so overwrite the passed-in conversion type with this underlying type
                var nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            else if (conversionType == typeof(Guid))
            {
                return new Guid(value.ToString());

            }
            else if (conversionType == typeof(long) && value is int)
            {
                //there is an issue with SQLite where the PK is ALWAYS int64. If this conversion type is Int64
                //we need to throw here - suggesting that they need to use LONG instead


                throw new InvalidOperationException("Can't convert an Int64 (long) to Int32(int). If you're using SQLite - this is probably due to your PK being an INTEGER, which is 64bit. You'll need to set your key to long.");
            }

            // Now that we've guaranteed conversionType is something Convert.ChangeType can handle (i.e. not a
            // nullable type), pass the call on to Convert.ChangeType
            return Convert.ChangeType(value, conversionType);
        }

        public static IDictionary<string, object> ToDictionary(this object value)
        {
            var result = new Dictionary<string, object>();
            var props = value.GetType().GetProperties();
            foreach (var pi in props)
            {
                try
                {
                    result.Add(pi.Name, pi.GetValue(value, null));
                }
                catch
                {
                    // ignored
                }
            }
            return result;
        }

        public static string ToHexString(this byte[] value, int length = 0)
        {
            if (value == null || value.Length <= 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            
            foreach (byte b in value)
            {
                sb.Append(b.ToString("x2"));

                if (length > 0 && sb.Length >= length)
                {
                    break;
                }
            }

            return sb.ToString();
        }

        public static byte[] Zip(this byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            using (var compressedStream = new MemoryStream())
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                zipStream.Write(buffer, 0, buffer.Length);
                zipStream.Close();
                return compressedStream.ToArray();
            }
        }

        public static byte[] Unzip(this byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            using (var compressedStream = new MemoryStream(buffer))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
    }
}
