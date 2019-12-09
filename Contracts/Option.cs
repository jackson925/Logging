using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class Option<T>
    {
        public bool IsError { get; set; }
        public bool HasValue => Value != null;
        public T Value { get; set; }

        public static Option<T> ToOption(T value)
        {
            return (value == null || string.IsNullOrEmpty(value.ToString()))
                ? ToErrorOption(default(T))
                : ToSuccessOption(value);
        }

        public static Option<T> ToErrorOption(T value)
        {
            return new Option<T> { IsError = true, Value = value };
        }

        public static Option<T> ToSuccessOption(T value)
        {
            return new Option<T> { IsError = false, Value = value };
        }
    }
}
