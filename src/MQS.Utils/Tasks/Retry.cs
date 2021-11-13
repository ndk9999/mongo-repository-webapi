using MQS.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQS.Utils.Tasks
{
	public static class Retry
	{
        public static void Run(Action operation, int attempts, TimeSpan? wait = null, Action<int, Exception> onFailed = null)
        {
            Guard.NotNull(operation, nameof(operation));

            Run(Operation, attempts, wait, onFailed);

            bool Operation()
            {
                operation();
                return true;
            }
        }

        public static T Run<T>(Func<T> operation, int attempts, TimeSpan? wait, Action<int, Exception> onFailed = null)
        {
            Guard.NotNull(operation, nameof(operation));

            if (attempts < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(attempts), attempts, "The maximum number of attempts must not be less than 1.");
            }

            var attempt = 0;

            while (true)
            {
                if (attempt > 0 && wait != null)
                {
                    Task.Delay(wait.Value).Wait();
                }

                try
                {
                    // Call the function passed in by the caller. 
                    return operation();
                }
                catch (Exception ex)
                {
                    attempt++;

                    onFailed?.Invoke(attempt, ex);

                    if (attempt >= attempts)
                    {
                        throw;
                    }
                }
            }
        }

        public static async Task RunAsync(Func<Task> operation, int attempts, TimeSpan? wait = null, Action<int, Exception> onFailed = null)
        {
            Guard.NotNull(operation, nameof(operation));

            async Task<bool> wrapper()
            {
                await operation();
                return true;
            }

            await RunAsync(wrapper, attempts, wait, onFailed);
        }

        public static async Task<T> RunAsync<T>(Func<Task<T>> operation, int attempts, TimeSpan? wait, Action<int, Exception> onFailed = null)
        {
            Guard.NotNull(operation, nameof(operation));

            if (attempts < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(attempts), attempts, "The maximum number of attempts must not be less than 1.");
            }

            var attempt = 0;

            while (true)
            {
                if (attempt > 0 && wait != null)
                {
                    await Task.Delay(wait.Value);
                }

                try
                {
                    // Call the function passed in by the caller. 
                    return await operation();
                }
                catch (Exception ex)
                {
                    attempt++;

                    onFailed?.Invoke(attempt, ex);

                    if (attempt >= attempts)
                    {
                        throw;
                    }
                }
            }
        }

    }
}
