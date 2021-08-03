using System;

namespace Core.Currencies.Exceptions
{
    public class CurrencyNotAvailableException : Exception
    {
        public CurrencyNotAvailableException(string message) : base(message)
        {
        }
    }
}