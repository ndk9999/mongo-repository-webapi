using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MQS.Utils.Extensions
{
	public static class ExceptionExtensions
	{
		public static bool IsFatal(this Exception ex)
		{
			return ex is StackOverflowException ||
				   ex is OutOfMemoryException ||
				   ex is AccessViolationException ||
				   ex is AppDomainUnloadedException ||
				   ex is ThreadAbortException ||
				   ex is SecurityException ||
				   ex is SEHException;
		}

		public static string GetRootErrorMessage(this Exception ex)
		{
			var message = ex.GetBaseException().Message;
			return string.IsNullOrWhiteSpace(message) || message.StartsWith("Exception of type") ? null : message;
		}
	}
}
