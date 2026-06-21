using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace MSMPSharp.Core;

public class MsmpClientBuilder
{
    private string? address;
    private int? port;
    private string? secret;
    private string? origin;
    private bool useTls;
    private RemoteCertificateValidationCallback? certValidator;

    internal MsmpClientBuilder() { }

    public MsmpClientBuilder WithHost(string address, int port)
    {
        this.address = address;
        this.port = port;
        return this;
    }

    public MsmpClientBuilder WithSecret(string secret)
    {
        this.secret = secret;
        return this;
    }

    public MsmpClientBuilder WithOrigin(string origin)
    {
        this.origin = origin;
        return this;
    }

    public MsmpClientBuilder WithTls(bool skipVerification = false, string? sha256Thumbprint = null)
    {
        this.useTls = true;

        if (skipVerification)
            certValidator = (_, _, _, _) => true;
        if(sha256Thumbprint != null)
        {
            sha256Thumbprint = sha256Thumbprint.Replace(":", "").Replace(" ", "").ToUpperInvariant();
            certValidator = (_, cert, _, _) =>
            {
                if (cert is null)
                    return false;

                // Compare the provided sha256 with the actual one from the certificate
                var actual = new X509Certificate2(cert).GetCertHashString(System.Security.Cryptography.HashAlgorithmName.SHA256);
                return actual.Equals(sha256Thumbprint, StringComparison.OrdinalIgnoreCase);
            };
        }

        return this;
    }

    public MsmpClient Build()
    {
        if (address == null || port == null)
            throw new InvalidOperationException("Host and port must be specified using WithHost() before building the client.");
        if(secret == null)
            throw new InvalidOperationException("Secret must be specified using WithSecret() before building the client.");

        return new MsmpClient(address, port.Value, secret, useTls, origin, certValidator);
    }
}