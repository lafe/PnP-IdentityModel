using System;

namespace SharePointPnP.IdentityModel.Extensions.S2S.Exceptions
{
    [Serializable]
    public class NotSupportedAlgorithmException : Exception
    {
        public NotSupportedAlgorithmException() { }
        public NotSupportedAlgorithmException(string message) : base(message) { }
        public NotSupportedAlgorithmException(string message, Exception inner) : base(message, inner) { }
        protected NotSupportedAlgorithmException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}