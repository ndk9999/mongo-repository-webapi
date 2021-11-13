using MQS.Utils.Extensions;
using System.IO;

namespace MQS.Utils.IO
{
	public static class FileHelper
	{
        public static bool DeleteIfExists(string filePath)
        {
            if (filePath.IsEmpty())
            {
                return false;
            }

            var exists = File.Exists(filePath);
            if (exists)
            {
                File.Delete(filePath);
            }

            return exists;
        }
    }
}
