using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClient.Helpers
{
    public static class LinqExt
    {
        public static async IAsyncEnumerable<T> WhereAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
        {
            foreach(var item in source)
            {
                if(await predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}