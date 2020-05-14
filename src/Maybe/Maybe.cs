using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Maybe
{
    public class Maybe<T> where T : class
    {
        [AllowNull]
        private readonly T _value;

        public static Maybe<T> Nothing = Return(null);

        public static Maybe<T> Return([AllowNull] T value)
        {
            return new Maybe<T>(value);
        }

        private Maybe([AllowNull] T value)
        {
            _value = value;
        }

        public Maybe<T> Bind(Func<T, Maybe<T>> func)
        {
            return _value is null ? Nothing : func(_value);
        }

        public void Bind(Action<T> action)
        {
            if (!(_value is null))
            {
                action(_value);
            }
        }

        public TResult Match<TResult>(Func<T, TResult> func, Func<TResult> notingFunc) 
            where TResult : class
        {
            return _value is null ? notingFunc() : func(_value);
        }

        protected bool Equals(Maybe<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_value, other._value);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Maybe<T>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(_value);
        }
    }
}
