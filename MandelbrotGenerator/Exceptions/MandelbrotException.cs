using System;

namespace MandelbrotGenerator.Exceptions
{
    public abstract class MandelbrotException : Exception
    {
        private protected MandelbrotException(string message) : base(message)
        {
        }
    }
}
