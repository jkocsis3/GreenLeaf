using System;
using System.Collections.Generic;
using System.Text;

namespace GreenLeaf.Core.Common
{
    public static class Guards
    {        
        public static void ArgumentGuard(string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException(CreateErrorMessage(str));
            }
            
        }

        public static void ArgumentGuard(long? lng)
        {
            if (lng == null || lng <= 0)
            {
                throw new ArgumentException(CreateErrorMessage(lng));
            }

        }

        public static void ArgumentGuard(double? dbl)
        {
            if (dbl == null || dbl <= 0)
            {
                throw new ArgumentException(CreateErrorMessage(dbl));
            }

        }

        public static void ArgumentGuard(int? i)
        {
            if (i == null || i <= 0)
            {
                throw new ArgumentException(CreateErrorMessage(i));
            }
        }

        public static void ArgumentGuard(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException(CreateErrorMessage(obj));
            }
        }

        private static string CreateErrorMessage(object obj)
        {
            return $"Argument {nameof(obj)} is invalid.";
        }
    }
}
