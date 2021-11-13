using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace MQS.Utils.Extensions
{
	public static class StringExtensions
	{
		[DebuggerStepThrough]
		public static bool IsCaseSensitiveEqual(this string value, string comparing)
		{
			return string.CompareOrdinal(value, comparing) == 0;
		}

		[DebuggerStepThrough]
		public static bool IsCaseInsensitiveEqual(this string value, string comparing)
		{
			return string.Compare(value, comparing, StringComparison.OrdinalIgnoreCase) == 0;
		}

		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EqualsNoCase(this string value, string other)
		{
			return string.Compare(value, other, StringComparison.OrdinalIgnoreCase) == 0;
		}

		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsEmpty(this string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}

		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasValue(this string value)
		{
			return !string.IsNullOrWhiteSpace(value);
		}

		[DebuggerStepThrough]
		public static bool ContainsAny(this string input, params string[] items)
		{
			return input.HasValue() && items.Any(input.Contains);
		}

		[DebuggerStepThrough]
		public static bool ContainsAny(this string input, StringComparison comparison, params string[] items)
		{
			return input.HasValue() && items.Any(x => input.Contains(x, comparison));
		}

		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string FormatInvariant(this string format, params object[] objects)
		{
			return string.Format(CultureInfo.InvariantCulture, format, objects);
		}

		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string FormatCurrent(this string format, params object[] objects)
		{
			return string.Format(CultureInfo.CurrentCulture, format, objects);
		}

		[DebuggerStepThrough]
		public static string Md5(this string value, Encoding encoding, bool toBase64 = false)
		{
			if (value.IsEmpty())
				return value;

			using (var md5 = MD5.Create())
			{
				byte[] data = encoding.GetBytes(value);

				if (toBase64)
				{
					byte[] hash = md5.ComputeHash(data);
					return Convert.ToBase64String(hash);
				}
				else
				{
					return md5.ComputeHash(data).ToHexString().ToLower();
				}
			}
		}

		public static string Sha(this string value, Encoding encoding)
		{
			if (value.HasValue())
			{
				using (var sha1 = new SHA1CryptoServiceProvider())
				{
					byte[] data = encoding.GetBytes(value);

					return sha1.ComputeHash(data).ToHexString();
				}
			}
			return "";
		}

		[DebuggerStepThrough]
		public static byte[] Compress(string text)
		{
			return Encoding.UTF8.GetBytes(text).Zip();
		}

	}
}
