using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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
    }
}
