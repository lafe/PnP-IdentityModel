using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using SharePointPnP.IdentityModel.Extensions.S2S.Exceptions;

namespace SharePointPnP.IdentityModel.Extensions.S2S.Tokens
{
    /// <summary>
    /// Factory that builds the necessary <see cref="SignatureProvider"/> based on the Cryptographic Service Provider we are getting from the 
    /// framework. Until .NET 4.6 the default Crypt Service Provider is <see cref="RSACryptoServiceProviderProxy"/>. Starting with .NET 4.7, we are getting RSACng.
    /// </summary>
    internal class AsymmetricSignatureProviderFactory
    {
        private const string EncryptionAlgorithm = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

        /// <summary>
        /// Creates a signature provider for an asymmetric encryption scheme. The 
        /// </summary>
        /// <param name="asymmetricSecurityKey">The <see cref="X509AsymmetricSecurityKey"/> asymmetric security key.</param>
        /// <returns>A <see cref="SignatureProvider"/> that uses the crypto service provider associated with the <paramref name="asymmetricSecurityKey"/></returns>
        /// <remarks>
        /// Until .NET 4.6 the default Crypt Service Provider is <see cref="RSACryptoServiceProviderProxy"/>. Starting with .NET 4.7, we are getting RSACng.
        /// </remarks>
        public static SignatureProvider CreateSignatureProvider(X509AsymmetricSecurityKey asymmetricSecurityKey)
        {
            Utility.VerifyNonNullArgument("asymmetricSecurityKey", asymmetricSecurityKey);
#if NET46
            if (!asymmetricSecurityKey.IsSupportedAlgorithm(EncryptionAlgorithm))
            {
                throw new NotSupportedAlgorithmException($"The given key does not support the algorithm \"{EncryptionAlgorithm}\"");
            }
            
            AsymmetricAlgorithm asymmetricAlgorithm;
            try
            {
                asymmetricAlgorithm = asymmetricSecurityKey.GetAsymmetricAlgorithm(EncryptionAlgorithm, true);
            }
            catch (CryptographicException ex)
            {
                throw new AsymmetricAlgorithmLoadingException($"The asymmetric algorithm \"{EncryptionAlgorithm}\" could not be loaded from the given key. Check if a private key exists and if the permissions of the private key are set correctly.", ex);
            }

            if (asymmetricAlgorithm is RSACryptoServiceProvider)
            {
                return new X509AsymmetricSignatureProvider(asymmetricSecurityKey);
            }
            if (asymmetricAlgorithm is RSACng)
            {
                return new X509RsaCngAsymmetricSignatureProvider(asymmetricSecurityKey);
            }

            throw new System.InvalidOperationException(string.Format("Could not get asymmetric signature provider of type \"{0}\"", asymmetricAlgorithm.GetType().Name));
#else
            //Older versions of the .NET Framework only know the RSACryptoServiceProvider. In this case, we can use the default implementation
            return new X509AsymmetricSignatureProvider(asymmetricSecurityKey);
#endif
        }
    }
}