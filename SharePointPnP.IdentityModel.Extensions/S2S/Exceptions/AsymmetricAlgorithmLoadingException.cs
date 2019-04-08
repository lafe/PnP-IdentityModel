namespace SharePointPnP.IdentityModel.Extensions.S2S.Exceptions
{
    [System.Serializable]
    public class AsymmetricAlgorithmLoadingException : System.Exception
    {
        public AsymmetricAlgorithmLoadingException() { }
        public AsymmetricAlgorithmLoadingException(string message) : base(message) { }
        public AsymmetricAlgorithmLoadingException(string message, System.Exception inner) : base(message, inner) { }
        protected AsymmetricAlgorithmLoadingException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}