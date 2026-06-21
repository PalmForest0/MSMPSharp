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

    /// <summary>
    /// Sets the host address and port for the MSMP client. This is required before building the client.<br/>
    /// These properties should correspond to your <c>server.properties</c> entries for <c>management-server-host</c> and <c>management-server-port</c>.
    /// </summary>
    /// <param name="address">The server management protocol address set on your server under <c>management-server-host</c>. Use "localhost" for local development.</param>
    /// <param name="port">The server management protocol port set on your server under <c>management-server-port</c>.</param>
    /// <returns>This client builder instance with the host address and port set.</returns>
    public MsmpClientBuilder WithHost(string address, int port)
    {
        this.address = address;
        this.port = port;
        return this;
    }

    /// <summary>
    /// Sets the secret for the MSMP client. This is required before building the client.<br/>
    /// This property should correspond to your <c>server.properties</c> entry for <c>management-server-secret</c>.
    /// </summary>
    /// <param name="secret">The server management protocol secret set on your server under <c>management-server-secret</c>.</param>
    /// <returns>This client builder instance with the secret set.</returns>
    public MsmpClientBuilder WithSecret(string secret)
    {
        this.secret = secret;
        return this;
    }

    /// <summary>
    /// Sets the origin for the MSMP client. This is optional and only needed if you are using a custom origin set under <c>management-server-allowed-origins</c>.
    /// </summary>
    /// <param name="origin">The origin for the MSMP client.</param>
    /// <returns>This client builder instance with the origin set.</returns>
    public MsmpClientBuilder WithOrigin(string origin)
    {
        this.origin = origin;
        return this;
    }

    /// <summary>
    /// Enables TLS for the MSMP client connection. By default, TLS is disabled.<br/>
    /// You can optionally skip certificate verification (not recommended for production) or specify a SHA256 thumbprint to validate the server's certificate against.
    /// </summary>
    /// <param name="skipVerification">Whether to skip certificate verification (only use for testing and development).</param>
    /// <param name="sha256Thumbprint">The SHA256 thumbprint of the expected server certificate.
    /// To get the thumbprint from a Java keystore, run the bash command:<br/>
    /// <c>keytool -list -v -keystore server.jks -storepass yourpassword</c><br/>
    /// Then copy the SHA-256 fingerprint from the output.</param>
    /// <returns>This client builder instance with TLS enabled.</returns>
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

    /// <summary>
    /// Builds the MSMP client with the specified configuration. Host address, port, and secret must be set before building.
    /// </summary>
    /// <returns>The created <see cref="MsmpClient"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the host, port, or secret is not specified.</exception>
    public MsmpClient Build()
    {
        if (address == null || port == null)
            throw new InvalidOperationException("Host and port must be specified using WithHost() before building the client.");
        if(secret == null)
            throw new InvalidOperationException("Secret must be specified using WithSecret() before building the client.");

        return new MsmpClient(address, port.Value, secret, useTls, origin, certValidator);
    }
}