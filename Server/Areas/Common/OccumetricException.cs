using System;

namespace Occumetric.Server.Areas.Common
{
    public class OccumetricException : Exception
    {
        public OccumetricException()
            : base()
        {
        }

        public OccumetricException(string message)
            : base(message)
        {
        }

        public OccumetricException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
