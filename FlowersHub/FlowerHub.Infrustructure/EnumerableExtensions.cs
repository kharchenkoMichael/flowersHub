using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlowersHub.Infrastructure
{
    public static class EnumerableExtensions
    {
        public static void ForEach(this IEnumerable items, Action<object> action)
        {
            foreach (var item in items)
            {
                action.Invoke(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action.Invoke(item);
            }
        }
        public static async Task ForEachAsync(this IEnumerable items, Func<object, Task> action)
        {
            foreach (var item in items)
            {
                await action.Invoke(item);
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> items, Func<T, Task> action)
        {
            foreach (var item in items)
            {
                await action.Invoke(item);
            }
        }
    }
}
