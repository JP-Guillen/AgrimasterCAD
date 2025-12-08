using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace AgrimasterCAD.Services;

public class R2StorageService
{
    public string? LastR2Error { get; private set; }

    private readonly string _accessKey;
    private readonly string _secretKey;
    private readonly string _bucket;
    private readonly string _accountId;
    private readonly string _publicBaseUrl;
    private readonly HttpClient _http = new();

    public R2StorageService(IConfiguration config)
    {
        _accessKey = config["R2:AccessKeyId"]!;
        _secretKey = config["R2:SecretAccessKey"]!;
        _bucket = config["R2:BucketName"]!;
        _accountId = config["R2:AccountId"]!;
        _publicBaseUrl = config["R2:PublicBaseUrl"]!;
    }

    public async Task<string?> UploadFileAsync(byte[] file, string key)
    {
        LastR2Error = null;

        string url = $"https://{_accountId}.r2.cloudflarestorage.com/{_bucket}/{key}";

        string payloadHash = ToHexString(SHA256.HashData(file));

        var req = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = new ByteArrayContent(file)
        };

        req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

        req.Headers.Add("x-amz-content-sha256", payloadHash);

        SignRequest(req, payloadHash);

        var res = await _http.SendAsync(req);

        if (!res.IsSuccessStatusCode)
        {
            LastR2Error = await res.Content.ReadAsStringAsync();
            return null;
        }

        return $"{_publicBaseUrl}/{key}";
    }

    private void SignRequest(HttpRequestMessage req, string payloadHash)
    {
        var now = DateTime.UtcNow;
        var amzDate = now.ToString("yyyyMMddTHHmmssZ");
        var dateStamp = now.ToString("yyyyMMdd");

        string region = "auto";
        string service = "s3";

        req.Headers.Host = req.RequestUri!.Host;
        req.Headers.Add("x-amz-date", amzDate);

        string canonicalHeaders =
            $"host:{req.Headers.Host}\n" +
            $"x-amz-content-sha256:{payloadHash}\n" +
            $"x-amz-date:{amzDate}\n";

        string signedHeaders = "host;x-amz-content-sha256;x-amz-date";

        string canonicalRequest =
            $"{req.Method}\n" +
            $"{req.RequestUri.AbsolutePath}\n" +
            "\n" +
            $"{canonicalHeaders}\n" +
            $"{signedHeaders}\n" +
            $"{payloadHash}";

        string canonicalRequestHash = ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(canonicalRequest)));

        string scope = $"{dateStamp}/{region}/{service}/aws4_request";

        string stringToSign =
            $"AWS4-HMAC-SHA256\n{amzDate}\n{scope}\n{canonicalRequestHash}";

        byte[] kDate = HmacSHA256(Encoding.UTF8.GetBytes("AWS4" + _secretKey), dateStamp);
        byte[] kRegion = HmacSHA256(kDate, region);
        byte[] kService = HmacSHA256(kRegion, service);
        byte[] kSigning = HmacSHA256(kService, "aws4_request");

        byte[] signatureBytes = HmacSHA256(kSigning, stringToSign);
        string signature = ToHexString(signatureBytes);

        string authHeader =
            $"AWS4-HMAC-SHA256 Credential={_accessKey}/{scope}, SignedHeaders={signedHeaders}, Signature={signature}";

        req.Headers.TryAddWithoutValidation("Authorization", authHeader);
    }

    private static byte[] HmacSHA256(byte[] key, string data)
    {
        using var hmac = new HMACSHA256(key);
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
    }

    private static string ToHexString(byte[] bytes)
        => BitConverter.ToString(bytes).Replace("-", "").ToLower();
}
