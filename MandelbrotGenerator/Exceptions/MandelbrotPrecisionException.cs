namespace MandelbrotGenerator.Exceptions
{
    public class MandelbrotPrecisionException : MandelbrotException
    {
        internal MandelbrotPrecisionException()
            : base("The desired area and resolution cannot be calculated precisely enough by the current implementation.")
        {
        }
    }
}
