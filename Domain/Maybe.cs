using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IMaybe<T>
    {
        TResult Match<TResult>(TResult nothing, Func<T, TResult> just);
    }
    public class Nothing<T> : IMaybe<T>
    {
        public TResult Match<TResult>(TResult nothing, Func<T, TResult> just)
        {
            return nothing;
        }
    }
    public class Just<T> : IMaybe<T> where T:class
    {
        private readonly T value;

        public Just(T value)
        {
            this.value = value;
        }

        public TResult Match<TResult>(TResult nothing, Func<T, TResult> just)
        {
            return just(value);
        }
    }
    public static class MaybeExtensions
    {
        public static bool IsNothing<T>(this IMaybe<T> m)
        {
            return m.Match<bool>(
                nothing: true,
                just: _ => false);
        }

        public static bool IsJust<T>(this IMaybe<T> m)
        {
            return m.Match<bool>(
                nothing: false,
                just: _ => true);
        }
        public static IMaybe<TResult> Select<T, TResult>(
         this IMaybe<T> source,
         Func<T, TResult> selector) where TResult:class
        {
            return source.Match<IMaybe<TResult>>(
                nothing: new Nothing<TResult>(),
                just: x => new Just<TResult>(selector(x)));
        }
        public static IEnumerable<T> Somethings<T>(this IEnumerable<IMaybe<T>> source)
        {
            return source.SelectMany(m => m.Match(new T[0], x => new[] { x }));
        }
    }
}
